using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Scalar.AspNetCore.Extensions;

internal sealed class ExcludeFromApiReferenceOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        var hasExcludeAttribute = context.Description.ActionDescriptor.EndpointMetadata.OfType<ExcludeFromApiReferenceAttribute>().Any();

        if (hasExcludeAttribute)
        {
            operation.Extensions.TryAdd(ScalarIgnore, new OpenApiBoolean(true));
        }

        return Task.CompletedTask;
    }
}