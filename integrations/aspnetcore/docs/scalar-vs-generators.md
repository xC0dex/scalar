# `Scalar.AspNetCore`: Enhancing OpenAPI Documentation

## Historical Context

Historically, OpenAPI generators like `Swashbuckle` and `NSwag` have provided an "all-in-one" solution, bundling both the documentation generation and an UI.

With the release of .NET 9, Microsoft introduced a new OpenAPI generator `Microsoft.AspNetCore.OpenApi` that focuses solely on the generation aspect, without including a built-in UI. This shift has created a gap for developers who previously relied on the integrated UI.

## Scalar.AspNetCore

`Scalar.AspNetCore` is designed to bridge this gap by offering beautiful API reference documentation. It provides a robust and customizable interface that enhances the usability and presentation of your API documentation. Key points to consider:

- **Traditional OpenAPI Generators**: Offer a combined solution with both backend generation and an integrated UI.
- **Microsoft.AspNetCore.OpenApi**: Focuses only on the generation aspect, leaving the UI component to be handled separately.
- **Scalar.AspNetCore**: Provides a comprehensive UI solution that complements the generation process, offering a more polished and user-friendly interface.

For more real-world examples and detailed guides, please refer to the following sections:

- [Authentication](./authentication.md)
- [Managing Multiple OpenAPI Documents](./multi-openapi-documents.md)
- [Referencing Multiple Scalar APIs](./multiple-scalar-api-references.md)
- [SubPath Deployment](./subpath-deployment.md)