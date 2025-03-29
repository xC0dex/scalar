using System.Collections.Immutable;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Lifecycle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using Yarp.ReverseProxy.Transforms;

namespace Scalar.Aspire;

internal sealed class ScalarHook(ResourceNotificationService notificationService, IServiceProvider serviceProvider) : IDistributedApplicationLifecycleHook, IAsyncDisposable
{
    private WebApplication? _app;
    public ValueTask DisposeAsync() => _app?.DisposeAsync() ?? ValueTask.CompletedTask;

    public async Task AfterEndpointsAllocatedAsync(DistributedApplicationModel appModel, CancellationToken cancellationToken = default)
    {
        var scalarResource = appModel.Resources.OfType<ScalarResource>().FirstOrDefault();
        
        if (scalarResource is null)
        {
            return;
        }

        var scalarAnnotations = scalarResource.Annotations.OfType<ScalarAnnotation>();

        

        var builder = WebApplication.CreateBuilder();

        // Todo: Unclear how to tackle ports and schemas
        // If any endpoint was configured, use them. Otherwise, use random ports
        var endpointAnnotations = scalarResource.Annotations.OfType<EndpointAnnotation>().Select(annotation => $"{annotation.UriScheme}://{annotation.TargetHost}:{annotation.Port}").ToArray();
        var hostUrls = endpointAnnotations.Length > 0 ? endpointAnnotations : ["http://*:0", "https://*:0"];

        builder.WebHost.UseUrls(hostUrls);
        builder.Services.AddHttpForwarder();

        _app = builder.Build();


        List<ScalarApiReferenceOptions> scalarConfigurations = [];
        foreach (var scalarAnnotation in scalarAnnotations)
        {
            var name = scalarAnnotation.ProjectResource.Name;
            
            using var scope = serviceProvider.CreateScope();
            var scalarOptions = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<ScalarOptions>>().Value;
            
            scalarAnnotation.ConfigureOptions?.Invoke(scalarOptions);

            // Only set OpenAPI servers if not already assigned
            scalarOptions.Servers ??= scalarAnnotation.ProjectResource.GetEndpoints().Select(endpoint => new ScalarServer(endpoint.Url)).ToList();
            
            //Todo: Not sure how we want to tackle this
            scalarOptions.OpenApiRoutePattern = $"{name}/{scalarOptions.OpenApiRoutePattern.TrimStart('/')}";
            
            // If no document names are provided, fallback to the default document name
            if (scalarOptions.Documents.Count == 0)
            {
                scalarOptions.AddDocument("v1");
            }
            
            scalarConfigurations.Add(scalarOptions);
            
            // Todo: Currently hard coded https
            var endpointReference = scalarAnnotation.ProjectResource.GetEndpoint("https");

            // scalarAnnotation.ConfigureOptions?.Invoke(scalarOptions);
            
            
            // _app.MapScalarApiReference($"/scalar/{name}", options =>
            // {
            //     options.OpenApiRoutePattern = $"{name}{options.OpenApiRoutePattern}";
            //     options.Servers = [new ScalarServer($"/{name}", name)];
            //     scalarAnnotation.ConfigureOptions?.Invoke(options);
            // });
            _app.MapForwarder($"/{name}/{{**catch-all}}", endpointReference.Url, context => context.AddPathRemovePrefix($"/{name}"));
        }
        
        // Todo: Should not be ScalarOptions
        var globalOptions = serviceProvider.GetRequiredService<IOptions<ScalarOptions>>().Value;
        
        
        _app.MapScalarApiReference("/", globalOptions, scalarConfigurations);


        await _app.StartAsync(cancellationToken);

        var addresses = _app.Services.GetRequiredService<IServer>().Features.GetRequiredFeature<IServerAddressesFeature>().Addresses;

        var urls = addresses.Select(url => new UrlSnapshot(url, url, false)).ToImmutableArray();

        await notificationService.PublishUpdateAsync(scalarResource, s => s with
        {
            State = "Running",
            Urls = urls,
            StartTimeStamp = DateTime.Now
        });
    }
}