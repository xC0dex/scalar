using System.Diagnostics.CodeAnalysis;

namespace Scalar.AspNetCore;

/// <summary>
/// Provides extension methods for configuring <see cref="ScalarOptions" />.
/// </summary>
public static partial class ScalarOptionsExtensions
{
    /// <summary>
    /// Sets the title of the page.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="title">The title to set.</param>
    public static TOptions WithTitle<TOptions>(this TOptions options, string title) where TOptions : IScalarEndpointOptions
    {
        options.Title = title;
        return options;
    }

    /// <summary>
    /// Sets the CDN URL for the API reference.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="url">The CDN URL to set.</param>
    public static TOptions WithCdnUrl<TOptions>(this TOptions options, [StringSyntax(StringSyntaxAttribute.Uri)] string url) where TOptions: IScalarEndpointOptions
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
    public static TOptions AddHeadContent<TOptions>(this TOptions options, [StringSyntax("html")] string headContent) where TOptions: IScalarEndpointOptions
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
    public static TOptions AddHeaderContent<TOptions>(this TOptions options, [StringSyntax("html")] string headerContent) where TOptions: IScalarEndpointOptions
    {
        options.HeaderContent += headerContent;
        return options;
    }
    
        /// <summary>
    /// Sets whether the base server URL should be dynamically determined based on the request context.
    /// </summary>
    /// <param name="options"><see cref="ScalarApiReferenceOptions" />.</param>
    /// <param name="dynamicBaseServerUrl">Whether to dynamically adjust the base server URL.</param>
    public static TOptions WithDynamicBaseServerUrl<TOptions>(this TOptions options, bool dynamicBaseServerUrl = true) where TOptions: IScalarEndpointOptions
    {
        options.DynamicBaseServerUrl = dynamicBaseServerUrl;
        return options;
    }
}