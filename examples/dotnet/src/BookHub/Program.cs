using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add dummy BookHub service
builder.Services.AddBookHub();

// Add API versioning and OpenAPI support
builder.Services.AddVersioningAndOpenApi();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/realms/master/.well-known/openid-configuration";
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();

// Map the default OpenAPI endpoint '/openapi/{documentName}.json'
app.MapOpenApi();

// Map the default Scalar API reference endpoint '/scalar'
app.MapScalarApiReference(options => options.Authentication = new ScalarAuthenticationOptions
{
    PreferredSecurityScheme = JwtBearerDefaults.AuthenticationScheme,
    Http = new HttpOptions
    {
        Bearer = new HttpBearerOptions
        {
            Token = "my JWT token"
        }
    }
});

// Map the book endpoints
app.MapBookEndpoints();
app.MapControllers();

app.Run();