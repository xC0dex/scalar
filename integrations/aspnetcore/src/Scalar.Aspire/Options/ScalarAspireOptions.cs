using System.Diagnostics.CodeAnalysis;
using Scalar.AspNetCore;

namespace Scalar.Aspire;

public sealed class ScalarAspireOptions : ScalarApiReferenceOptions, IScalarEndpointOptions
{
    /// <summary>
    /// Adds a default proxy to prevent CORS issues.
    /// </summary>
    /// <value>The default value is <c>true</c>.</value>
    /// <remarks>
    /// The YARP proxy is used to bypass CORS issues by forwarding requests to the server, thus circumventing same-origin restrictions.
    /// This is particularly useful in development environments where frontend and backend servers run on different hosts.
    /// </remarks>
    public bool AutoProxy { get; set; } = true;

    /// <inheritdoc />
    public string? Title { get; set; } = "Scalar API Reference";

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