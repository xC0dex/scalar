# Authentication in Scalar.AspNetCore

## Understanding Authentication in OpenAPI Documents

In the .NET ecosystem, there is a distinction between authentication mechanisms and their representation in OpenAPI documents. Simply adding an authentication scheme to the Dependency Injection (DI) container does not automatically include it in the OpenAPI document.

## How Scalar Works

Scalar.AspNetCore relies solely on the OpenAPI document to determine the security schemes and requirements. If the OpenAPI document does not specify any security schemes or requirements, Scalar will not display any authentication options.

To ensure that the necessary authentication schemes are included in the OpenAPI document, you need to use an `OpenApiDocumentTransformer`. This tool allows you to add the required security schemes to the OpenAPI document, ensuring that Scalar.AspNetCore can recognize and display them appropriately.

There is an [open GitHub issue](https://github.com/dotnet/aspnetcore/issues/39761) for automating this with .NET 10.

In the following sections, we will explain how to implement an `OpenApiDocumentTransformer` to add authentication schemes to your OpenAPI document.