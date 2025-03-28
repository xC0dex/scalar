using Scalar.Aspire;
using Scalar.AspNetCore;

var builder = DistributedApplication.CreateBuilder(args);

var bookstore = builder.AddProject<Projects.Scalar_AspNetCore_Playground>("bookstore");
var bookstoreTwo = builder.AddProject<Projects.Scalar_AspNetCore_Playground>("bookstore-second");

var scalar = builder.AddScalarApiReference(options =>
{
    options.CdnUrl = "https://cdn.jsdelivr.net/npm/@scalar/api-reference";
    options.WithTheme(ScalarTheme.Mars);
}).WithHttpsEndpoint(port: 54678);

scalar
    .WithReference(bookstore, options =>
    {
        options.AddDocuments("v1", "v2");
    })
    .WithReference(bookstoreTwo, options =>
    {
        options.OpenApiRoutePattern = "swagger/{documentName}/swagger.json";
    });

builder.Build().Run();