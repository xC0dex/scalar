namespace Scalar.AspNetCore;

/// <summary>
/// Defines the options used to configure the Scalar API reference endpoint.
/// </summary>
public interface IScalarEndpointOptions
{
    /// <summary>
    /// Gets or sets the title of the HTML document.
    /// </summary>
    /// <value>The default value is <c>'Scalar API Reference'</c>.</value>
    /// <remarks>Use the <c>{documentName}</c> placeholder to include the document name in the title.</remarks>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the CDN URL for the API reference.
    /// </summary>
    /// <value>The default value is <c>null</c></value>
    /// <remarks>Use this option to load the API reference from a different CDN or local server.</remarks>
    public string? CdnUrl { get; set; }

    /// <summary>
    /// Gets or sets additional content to be included in the head section of the HTML document.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    public string? HeadContent { get; set; }

    /// <summary>
    /// Gets or sets the HTML content to be rendered in the header section of the page.
    /// This content will be embedded after the <c>&lt;body&gt;</c> tag and before the API reference.
    /// </summary>
    /// <example>
    /// The following is an example of how to use this property:
    /// <code>HeaderContent = "&lt;header&gt;Welcome to my API reference&lt;/header&gt;";</code>
    /// renders the following HTML:
    /// <code>
    /// <![CDATA[
    /// <body>
    ///     <header>Welcome to my API reference</header>
    ///     <div id="app"></div>
    /// </body>
    /// ]]>
    /// </code>
    /// </example>
    public string? HeaderContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the base server URL should be dynamically determined based on the request context.
    /// </summary>
    /// <value>The default value is <c>false</c>.</value>
    /// <remarks>
    /// When set to <c>true</c>, the <see cref="ScalarApiReferenceOptions.BaseServerUrl" /> property will be overwritten and the base server URL will be dynamically
    /// adjusted based on the request context. This only works for relative server URLs.
    /// </remarks>
    public bool DynamicBaseServerUrl { get; set; }
}