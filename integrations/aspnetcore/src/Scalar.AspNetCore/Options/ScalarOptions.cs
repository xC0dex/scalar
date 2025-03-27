using System.Diagnostics.CodeAnalysis;

namespace Scalar.AspNetCore;

/// <summary>
/// Represents all available options for the Scalar API reference.
/// Based on <a href="https://github.com/scalar/scalar/blob/main/documentation/configuration.md">Configuration</a>.
/// </summary>
public sealed class ScalarOptions : ScalarApiReferenceOptions, IScalarEndpointOptions
{
    /// <inheritdoc />
    public string? Title { get; set; } = "Scalar API Reference";

    /// <summary>
    /// Path prefix to access the documentation.
    /// </summary>
    /// <value>The default value is <c>'/scalar'</c>.</value>
    /// <remarks>
    /// This property is obsolete and will be removed in a future release. Please use the 'endpointPrefix' parameter of the <see cref="ScalarEndpointRouteBuilderExtensions.MapScalarApiReference(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder)" /> method instead.
    /// </remarks>
    [Obsolete("This property is obsolete and will be removed in a future release. Please use the 'endpointPrefix' parameter of the 'MapScalarApiReference' method instead.")]
    public string EndpointPathPrefix { get; set; } = "/scalar";

    /// <inheritdoc />
    [StringSyntax(StringSyntaxAttribute.Uri)]
    public string? CdnUrl { get; set; }

    /// <inheritdoc />
    [StringSyntax("html")]
    public string? HeadContent { get; set; }

    /// <inheritdoc />
    [StringSyntax("html")]
    public string? HeaderContent { get; set; }
    
    /// <inheritdoc />
    public bool DynamicBaseServerUrl { get; set; }
}