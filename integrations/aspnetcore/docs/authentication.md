# Authentication in Scalar.AspNetCore

## Understanding Authentication in OpenAPI Documents

In the .NET ecosystem, there is a distinction between authentication mechanisms and their representation in OpenAPI documents. Simply adding an authentication scheme to the Dependency Injection (DI) container does not automatically include it in the OpenAPI document.

## How Scalar Works

Scalar.AspNetCore relies solely on the OpenAPI document to determine the security schemes and requirements. If the OpenAPI document does not specify any security schemes or requirements, Scalar will not display any authentication options.

To ensure that the necessary authentication schemes are included in the OpenAPI document, you need to use an `OpenApiDocumentTransformer`. This tool allows you to add the required security schemes to the OpenAPI document, ensuring that Scalar.AspNetCore can recognize and display them appropriately.

There is an [open GitHub issue](https://github.com/dotnet/aspnetcore/issues/39761) for automating this with .NET 10.

In the following sections, we will explain how to implement an `OpenApiDocumentTransformer` to add authentication schemes to your OpenAPI document.

## Bearer Authentication

To set up Bearer authentication in a .NET API, you can start by configuring the authentication scheme in the Dependency Injection (DI) container:

```csharp
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/realms/master/.well-known/openid-configuration";
});
```

Next, you need to add this security scheme to the OpenAPI document so that Scalar can recognize and use it. This can be achieved by creating a custom `OpenApiDocumentTransformer`:

```csharp
options.AddDocumentTransformer((document, _, _) =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "bearer"
    };
    document.Components ??= new OpenApiComponents();
    document.Components.SecuritySchemes.Add(JwtBearerDefaults.AuthenticationScheme, securityScheme);
    return Task.CompletedTask;
});
```

This transformer ensures that the required security scheme is included in the generated OpenAPI document. Once you start your project, you will see an option in the authentication dropdown.

To make the usage of this scheme easier, you can configure it in the `MapScalarApiReference()` method:

```csharp
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
```

This configuration preselects the `Bearer` authentication scheme in the UI and pre-fills the token with a given value, simplifying the authentication process for users.