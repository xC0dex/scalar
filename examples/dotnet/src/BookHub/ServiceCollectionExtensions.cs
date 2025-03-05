using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace BookHub;

internal static class ServiceCollectionExtensions
{
    internal static void AddVersioningAndOpenApi(this IServiceCollection services)
    {
        // Add default api versioning
        services.AddApiVersioning().AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            // Replace 'v{version:apiVersion}' with the actual version in the URL
            options.SubstituteApiVersionInUrl = true;
        });

        // We have two versions of the API
        string[] versions = ["v1", "v2"];

        foreach (var version in versions)
            // Add OpenAPI documentation for each version
            services.AddOpenApi(version, options =>
            {
                // Map the version to the OpenAPI document
                options.AddDocumentTransformer((document, context, _) =>
                {
                    var descriptionProvider = context.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    var versionDescription = descriptionProvider.ApiVersionDescriptions.FirstOrDefault(x => x.GroupName == version);
                    document.Info.Version = versionDescription?.ApiVersion.ToString();
                    return Task.CompletedTask;
                });

                // Remove the default server because we want to Scalar to use the server URL from the request
                options.AddDocumentTransformer((document, _, _) =>
                {
                    document.Servers = [];
                    return Task.CompletedTask;
                });

                options.AddDocumentTransformer((document, _, _) =>
                {
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri("http://localhost:8080/realms/master/protocol/openid-connect/auth"),
                                TokenUrl = new Uri("http://localhost:8080/realms/master/protocol/openid-connect/token")
                            }
                        },
                        In = ParameterLocation.Header,
                        Scheme = "bearer"
                        // Scheme = "bearer",
                        // BearerFormat = "JWT",
                        // In = ParameterLocation.Header,
                    };
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes.Add(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                    return Task.CompletedTask;
                });
            });
    }
}