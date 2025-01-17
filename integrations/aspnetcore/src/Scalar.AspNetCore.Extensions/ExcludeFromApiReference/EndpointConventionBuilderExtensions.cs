using Microsoft.AspNetCore.Builder;

namespace Scalar.AspNetCore.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IEndpointConventionBuilder" />.
/// </summary>
public static class EndpointConventionBuilderExtensions
{
    /// <summary>
    /// Excludes the endpoint from the Scalar API reference.
    /// </summary>
    /// <typeparam name="TBuilder">The type of <see cref="IEndpointConventionBuilder" />.</typeparam>
    /// <param name="builder">The endpoint convention builder.</param>
    public static TBuilder ExcludeFromApiReference<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.WithMetadata(new ExcludeFromApiReferenceAttribute());
        return builder;
    }
}