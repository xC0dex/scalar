using System.Diagnostics.CodeAnalysis;

namespace Scalar.AspNetCore;

public static partial class ScalarOptionsExtensions
{
    /// <summary>
    /// Sets the path prefix to access the documentation.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="prefix">The path prefix to set.</param>
    /// <remarks>
    /// This method is obsolete and will be removed in a future release. Please use the 'endpointPrefix' parameter of the <see cref="ScalarEndpointRouteBuilderExtensions.MapScalarApiReference(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder)" /> method instead.
    /// </remarks>
    [Obsolete("This method is obsolete and will be removed in a future release. Please use the 'endpointPrefix' parameter of the 'MapScalarApiReference' method instead.")]
    public static ScalarOptions WithEndpointPrefix(this ScalarOptions options, string prefix)
    {
        options.EndpointPathPrefix = prefix;
        return options;
    }

    /// <summary>
    /// Sets the title of the page.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="title">The title to set.</param>
    public static ScalarOptions WithTitle(this ScalarOptions options, string title)
    {
        options.Title = title;
        return options;
    }

    /// <summary>
    /// Sets the CDN URL for the API reference.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="url">The CDN URL to set.</param>
    public static ScalarOptions WithCdnUrl(this ScalarOptions options, [StringSyntax(StringSyntaxAttribute.Uri)] string url)
    {
        options.CdnUrl = url;
        return options;
    }

    /// <summary>
    /// Sets additional HTML content to be included in the head section of the HTML document.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="headContent">The additional content to include in the head section.</param>
    /// <remarks>
    /// The provided content will be appended.
    /// </remarks>
    public static ScalarOptions AddHeadContent(this ScalarOptions options, [StringSyntax("html")] string headContent)
    {
        options.HeadContent += headContent;
        return options;
    }

    /// <summary>
    /// Adds additional HTML content to be rendered in the header section of the page.
    /// This content will be embedded after the <c>&lt;body&gt;</c> tag and before the API reference.
    /// </summary>
    /// <example>
    /// The following is an example of how to use this property:
    /// <code>AddHeaderContent("&lt;header&gt;Welcome to my API reference&lt;/header&gt;");</code>
    /// renders the following HTML:
    /// <code>
    /// <![CDATA[
    /// <body>
    ///     <header>Welcome to my API reference</header>
    ///     <script id="api-reference"></script>
    /// </body>
    /// ]]>
    /// </code>
    /// </example>
    /// <remarks>The provided content will be appended.</remarks>
    public static ScalarOptions AddHeaderContent(this ScalarOptions options, [StringSyntax("html")] string headerContent)
    {
        options.HeaderContent += headerContent;
        return options;
    }

    /// <summary>
    /// Sets whether the base server URL should be dynamically determined based on the request context.
    /// </summary>
    /// <param name="options"><see cref="ScalarApiReferenceOptions" />.</param>
    /// <param name="dynamicBaseServerUrl">Whether to dynamically adjust the base server URL.</param>
    public static ScalarOptions WithDynamicBaseServerUrl(this ScalarOptions options, bool dynamicBaseServerUrl = true)
    {
        options.DynamicBaseServerUrl = dynamicBaseServerUrl;
        return options;
    }
}