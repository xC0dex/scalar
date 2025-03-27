using Scalar.Aspire;
using Scalar.AspNetCore;

var builder = DistributedApplication.CreateBuilder(args);

var bookstore = builder.AddProject<Projects.Scalar_AspNetCore_Playground>("bookstore");

var scalar = builder.AddScalarApiReference(options =>
{
    options.AutoProxy = true;
    options.CdnUrl = "https://cdn.jsdelivr.net/npm/@scalar/api-reference";
    options.Theme = ScalarTheme.DeepSpace;
    options.Title = "Aspire API Reference";
    options
        .WithPreferredScheme("ApiKey")
        .WithApiKeyAuthentication(x => x.Token = "my-api-key");
}).WithHttpsEndpoint(port: 54678, isProxied: false);

scalar.WithReference(bookstore, options =>
{
    options.AddDocuments("v1", "v2");
});

builder.Build().Run();