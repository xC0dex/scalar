var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add dummy BookHub service
builder.Services.AddBookHub();

// Add API versioning and OpenAPI support
builder.Services.AddVersioningAndOpenApi();

var app = builder.Build();

// Map the default OpenAPI endpoint '/openapi/{documentName}.json'
app.MapOpenApi();

// Map the default Scalar API reference endpoint '/scalar'
app.MapScalarApiReference();

// Map the book endpoints
app.MapBookEndpoints();
app.MapControllers();

app.Run();