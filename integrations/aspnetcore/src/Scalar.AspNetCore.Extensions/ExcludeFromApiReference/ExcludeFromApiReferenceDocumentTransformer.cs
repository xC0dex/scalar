using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Scalar.AspNetCore.Extensions;

internal sealed class ExcludeFromApiReferenceDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var tagOperations = new Dictionary<string, List<OpenApiOperation>>();

        // Group operations by tag
        foreach (var path in document.Paths)
        {
            foreach (var (_, operation) in path.Value.Operations)
            {
                var tags = operation.Tags ?? [];

                foreach (var tagName in tags.Select(tag => tag.Name))
                {
                    if (!tagOperations.TryGetValue(tagName, out var operations))
                    {
                        operations = [];
                        tagOperations[tagName] = operations;
                    }

                    operations.Add(operation);
                }
            }
        }

        // Find all tags that should be fully ignored
        var tagsToExclude = tagOperations.Where(kvp => kvp.Value.All(operation => operation.Extensions.ContainsKey(ScalarIgnore)));

        foreach (var (tag, operations) in tagsToExclude)
        {
            var tagToExclude = document.Tags.FirstOrDefault(t => t.Name == tag);
            tagToExclude?.Extensions.TryAdd(ScalarIgnore, new OpenApiBoolean(true));

            // Remove the ignore extension from all operations
            foreach (var openApiOperation in operations)
            {
                openApiOperation.Extensions.Remove(ScalarIgnore);
            }
        }

        return Task.CompletedTask;
    }
}