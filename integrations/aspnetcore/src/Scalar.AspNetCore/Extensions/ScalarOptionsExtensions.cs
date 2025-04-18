using System.Diagnostics.CodeAnalysis;

namespace Scalar.AspNetCore;

/// <summary>
/// Provides extension methods for configuring <see cref="ScalarOptions" />.
/// </summary>
public static class ScalarOptionsExtensions
{
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
    /// Sets the favicon path or URL that will be used for the documentation.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="favicon">The path or URL to the favicon.</param>
    public static ScalarOptions WithFavicon(this ScalarOptions options, string favicon)
    {
        options.Favicon = favicon;
        return options;
    }

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
    /// Adds the specified OpenAPI document to the Scalar API reference.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="documentName">The name identifier for the OpenAPI document. This value will be used to replace the '{documentName}' placeholder in the <see cref="ScalarOptions.OpenApiRoutePattern"/>.</param>
    /// <param name="title">Optional display title for the document. If not provided, the document name will be used as the title.</param>
    /// <param name="routePattern">Optional route pattern for the OpenAPI document. If not provided, the <see cref="ScalarOptions.OpenApiRoutePattern"/> will be used. The pattern can include the '{documentName}' placeholder which will be replaced with the document name.</param>
    /// <remarks>
    /// When multiple documents are added, they will be displayed as selectable options in a dropdown menu.
    /// If no documents are explicitly added, a default document named 'v1' will be used.
    /// The '{documentName}' placeholder in the route pattern will be replaced with the provided document name.
    /// </remarks>
    public static ScalarOptions AddDocument(this ScalarOptions options, string documentName, string? title = null, string? routePattern = null)
    {
        options.Documents.Add(new ScalarDocument(documentName, title, routePattern));
        return options;
    }

    /// <summary>
    /// Adds the specified OpenAPI documents to the Scalar API reference.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="documentNames">The name identifiers for the OpenAPI documents.</param>
    /// <remarks>
    /// When multiple documents are added, they will be displayed as selectable options in a dropdown menu.
    /// If no documents are explicitly added, a default document named 'v1' will be used.
    /// </remarks>
    public static ScalarOptions AddDocuments(this ScalarOptions options, params IEnumerable<string> documentNames)
    {
        var documents = documentNames.Select(documentName => new ScalarDocument(documentName));
        options.Documents.AddRange(documents);
        return options;
    }

    /// <summary>
    /// Adds the specified OpenAPI documents to the Scalar API reference.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="documents">A list of <see cref="ScalarDocument" />`s to add.</param>
    /// <remarks>
    /// When multiple documents are added, they will be displayed as selectable options in a dropdown menu.
    /// If no documents are explicitly added, a default document named 'v1' will be used.
    /// </remarks>
    public static ScalarOptions AddDocuments(this ScalarOptions options, params IEnumerable<ScalarDocument> documents)
    {
        options.Documents.AddRange(documents);
        return options;
    }

    /// <summary>
    /// Sets the proxy URL for the API requests.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="proxyUrl">The proxy URL to set.</param>
    public static ScalarOptions WithProxyUrl(this ScalarOptions options, [StringSyntax(StringSyntaxAttribute.Uri)] string proxyUrl)
    {
        options.ProxyUrl = proxyUrl;
        return options;
    }

    /// <summary>
    /// Sets whether the sidebar should be shown.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="showSidebar">Whether to show the sidebar.</param>
    public static ScalarOptions WithSidebar(this ScalarOptions options, bool showSidebar)
    {
        options.ShowSidebar = showSidebar;
        return options;
    }

    /// <summary>
    /// Sets whether models should be shown in the sidebar, search, and content.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="showModels">Whether to show models.</param>
    public static ScalarOptions WithModels(this ScalarOptions options, bool showModels)
    {
        options.HideModels = !showModels;
        return options;
    }

    /// <summary>
    /// Sets whether to show the "Download OpenAPI Specification" button.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="showDownloadButton">Whether to show the download button.</param>
    public static ScalarOptions WithDownloadButton(this ScalarOptions options, bool showDownloadButton)
    {
        options.HideDownloadButton = !showDownloadButton;
        return options;
    }

    /// <summary>
    /// Sets whether to show the "Test Request" button.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="showTestRequestButton">Whether to show the test request button.</param>
    public static ScalarOptions WithTestRequestButton(this ScalarOptions options, bool showTestRequestButton)
    {
        options.HideTestRequestButton = !showTestRequestButton;
        return options;
    }

    /// <summary>
    /// Sets whether dark mode is on or off initially.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="darkMode">Whether dark mode is on or off initially.</param>
    public static ScalarOptions WithDarkMode(this ScalarOptions options, bool darkMode)
    {
        options.DarkMode = darkMode;
        return options;
    }

    /// <summary>
    /// Forces the theme mode to always be the specified state.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="forceThemeMode">The theme mode to force.</param>
    public static ScalarOptions WithForceThemeMode(this ScalarOptions options, ThemeMode forceThemeMode)
    {
        options.ForceThemeMode = forceThemeMode;
        return options;
    }

    /// <summary>
    /// Sets whether to show the dark mode toggle.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="showDarkModeToggle">Whether to show the dark mode toggle.</param>
    public static ScalarOptions WithDarkModeToggle(this ScalarOptions options, bool showDarkModeToggle)
    {
        options.HideDarkModeToggle = !showDarkModeToggle;
        return options;
    }

    /// <summary>
    /// Sets custom CSS directly to the component.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="customCss">The custom CSS to set.</param>
    public static ScalarOptions WithCustomCss(this ScalarOptions options, [StringSyntax("css")] string customCss)
    {
        options.CustomCss = customCss;
        return options;
    }

    /// <summary>
    /// Sets the key used with CTRL/CMD to open the search modal.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="searchHotKey">The search hotkey to set.</param>
    public static ScalarOptions WithSearchHotKey(this ScalarOptions options, string searchHotKey)
    {
        options.SearchHotKey = searchHotKey;
        return options;
    }

    /// <summary>
    /// Sets the color theme.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="theme">The theme to set.</param>
    public static ScalarOptions WithTheme(this ScalarOptions options, ScalarTheme theme)
    {
        options.Theme = theme;
        return options;
    }

    /// <summary>
    /// Sets the layout for the Scalar API reference.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="layout">The layout to use.</param>
    public static ScalarOptions WithLayout(this ScalarOptions options, ScalarLayout layout)
    {
        options.Layout = layout;
        return options;
    }

    /// <summary>
    /// Sets whether to use the default fonts.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="useDefaultFonts">Whether to use the default fonts.</param>
    public static ScalarOptions WithDefaultFonts(this ScalarOptions options, bool useDefaultFonts)
    {
        options.DefaultFonts = useDefaultFonts;
        return options;
    }

    /// <summary>
    /// Sets whether to open all tags by default.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="useOpenAllTags">Whether to open all tags by default.</param>
    public static ScalarOptions WithDefaultOpenAllTags(this ScalarOptions options, bool useOpenAllTags)
    {
        options.DefaultOpenAllTags = useOpenAllTags;
        return options;
    }

    /// <summary>
    /// Adds a server to the list of servers in the <see cref="ScalarOptions" />.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="server">The <see cref="ScalarServer" /> to add.</param>
    public static ScalarOptions AddServer(this ScalarOptions options, ScalarServer server)
    {
        options.Servers ??= new List<ScalarServer>();
        options.Servers.Add(server);
        return options;
    }

    /// <summary>
    /// Adds a server to the list of servers in the <see cref="ScalarOptions" /> using a URL.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="url">The URL of the server to add.</param>
    public static ScalarOptions AddServer(this ScalarOptions options, [StringSyntax(StringSyntaxAttribute.Uri)] string url) => options.AddServer(new ScalarServer(url));

    /// <summary>
    /// Adds a server to the list of servers in the <see cref="ScalarOptions" /> using a URL and description.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="url">The URL of the server to add.</param>
    /// <param name="description">The description of the server.</param>
    public static ScalarOptions AddServer(this ScalarOptions options, [StringSyntax(StringSyntaxAttribute.Uri)] string url, string description) => options.AddServer(new ScalarServer(url, description));

    /// <summary>
    /// Adds metadata to the configuration.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="key">The metadata key.</param>
    /// <param name="value">The metadata value.</param>
    public static ScalarOptions AddMetadata(this ScalarOptions options, string key, string value)
    {
        options.Metadata ??= new Dictionary<string, string>();
        options.Metadata.Add(key, value);
        return options;
    }

    /// <summary>
    /// Sets the tag sorter for the <see cref="ScalarOptions" />.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="tagSorter">The <see cref="TagSorter" /> to use.</param>
    public static ScalarOptions WithTagSorter(this ScalarOptions options, TagSorter tagSorter)
    {
        options.TagSorter = tagSorter;
        return options;
    }

    /// <summary>
    /// Sets the operation sorter for the <see cref="ScalarOptions" />.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="operationSorter">The <see cref="OperationSorter" /> to use.</param>
    public static ScalarOptions WithOperationSorter(this ScalarOptions options, OperationSorter operationSorter)
    {
        options.OperationSorter = operationSorter;
        return options;
    }

    /// <summary>
    /// Sets the preferred authentication scheme.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="preferredScheme">The preferred authentication scheme.</param>
    public static ScalarOptions WithPreferredScheme(this ScalarOptions options, string preferredScheme)
    {
        options.Authentication ??= new ScalarAuthenticationOptions();
        options.Authentication.PreferredSecurityScheme = preferredScheme;
        return options;
    }

    /// <summary>
    /// Sets the API key authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="apiKeyOptions">The API key options to set.</param>
    public static ScalarOptions WithApiKeyAuthentication(this ScalarOptions options, ApiKeyOptions apiKeyOptions)
    {
        options.Authentication ??= new ScalarAuthenticationOptions();
        options.Authentication.ApiKey = apiKeyOptions;
        return options;
    }

    /// <summary>
    /// Configures the API key authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="configureApiKeyOptions">The action to configure the API key options.</param>
    public static ScalarOptions WithApiKeyAuthentication(this ScalarOptions options, Action<ApiKeyOptions> configureApiKeyOptions)
    {
        var apiKeyOptions = new ApiKeyOptions();
        configureApiKeyOptions(apiKeyOptions);
        return options.WithApiKeyAuthentication(apiKeyOptions);
    }

    /// <summary>
    /// Configures the OAuth2 authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="oauth2Options">The OAuth2 options to set.</param>
    public static ScalarOptions WithOAuth2Authentication(this ScalarOptions options, OAuth2Options oauth2Options)
    {
        options.Authentication ??= new ScalarAuthenticationOptions();
        options.Authentication.OAuth2 = oauth2Options;
        return options;
    }

    /// <summary>
    /// Configures the OAuth2 authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="configureOAuth2Options">The action to configure the OAuth2 options.</param>
    public static ScalarOptions WithOAuth2Authentication(this ScalarOptions options, Action<OAuth2Options> configureOAuth2Options)
    {
        var oauth2Options = new OAuth2Options();
        configureOAuth2Options(oauth2Options);
        return options.WithOAuth2Authentication(oauth2Options);
    }

    /// <summary>
    /// Sets the HTTP basic authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="httpBasicOptions">The HTTP basic options to set.</param>
    public static ScalarOptions WithHttpBasicAuthentication(this ScalarOptions options, HttpBasicOptions httpBasicOptions)
    {
        options.Authentication ??= new ScalarAuthenticationOptions();
        options.Authentication.Http ??= new HttpOptions();
        options.Authentication.Http.Basic = httpBasicOptions;
        return options;
    }

    /// <summary>
    /// Configures the HTTP basic authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="configureHttpBasicOptions">The action to configure the HTTP basic options.</param>
    public static ScalarOptions WithHttpBasicAuthentication(this ScalarOptions options, Action<HttpBasicOptions> configureHttpBasicOptions)
    {
        var httpBasicOptions = new HttpBasicOptions();
        configureHttpBasicOptions(httpBasicOptions);
        return options.WithHttpBasicAuthentication(httpBasicOptions);
    }

    /// <summary>
    /// Sets the HTTP bearer authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="httpBearerOptions">The HTTP bearer options to set.</param>
    public static ScalarOptions WithHttpBearerAuthentication(this ScalarOptions options, HttpBearerOptions httpBearerOptions)
    {
        options.Authentication ??= new ScalarAuthenticationOptions();
        options.Authentication.Http ??= new HttpOptions();
        options.Authentication.Http.Bearer = httpBearerOptions;
        return options;
    }

    /// <summary>
    /// Configures the HTTP bearer authentication options.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="configureHttpBearerOptions">The action to configure the HTTP bearer options.</param>
    public static ScalarOptions WithHttpBearerAuthentication(this ScalarOptions options, Action<HttpBearerOptions> configureHttpBearerOptions)
    {
        var httpBearerOptions = new HttpBearerOptions();
        configureHttpBearerOptions(httpBearerOptions);
        return options.WithHttpBearerAuthentication(httpBearerOptions);
    }

    /// <summary>
    /// Sets the default HTTP client.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="target">The target to set.</param>
    /// <param name="client">The client to set.</param>
    public static ScalarOptions WithDefaultHttpClient(this ScalarOptions options, ScalarTarget target, ScalarClient client)
    {
        options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(target, client);
        return options;
    }

    /// <summary>
    /// Sets the route pattern of the OpenAPI document.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="pattern">The route pattern to set.</param>
    public static ScalarOptions WithOpenApiRoutePattern(this ScalarOptions options, [StringSyntax("Route")] string pattern)
    {
        options.OpenApiRoutePattern = pattern;
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
    /// Sets whether to expose 'dotnet' to the configuration.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="expose">Whether to expose 'dotnet'.</param>
    public static ScalarOptions WithDotNetFlag(this ScalarOptions options, bool expose)
    {
        options.DotNetFlag = expose;
        return options;
    }

    /// <summary>
    /// Sets whether the client button from the reference sidebar should be shown.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="showButton">Whether to show the client button.</param>
    public static ScalarOptions WithClientButton(this ScalarOptions options, bool showButton)
    {
        options.HideClientButton = !showButton;
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
    /// Sets the base server URL that will be used to prefix all relative OpenAPI server URLs.
    /// </summary>
    /// <value>The default value is <c>null</c>.</value>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="baseServerUrl">The base server URL to add.</param>
    /// <remarks>
    /// When specified, this URL will be prepended to all relative server URLs defined in the OpenAPI document.
    /// For example, if BaseServerUrl is "https://api.example.com" and a server URL in the OpenAPI document is
    /// "/api", the resulting URL will be "https://api.example.com/api". This only affects relative server URLs;
    /// absolute URLs remain unchanged.
    /// </remarks>
    public static ScalarOptions WithBaseServerUrl(this ScalarOptions options, [StringSyntax(StringSyntaxAttribute.Uri)] string baseServerUrl)
    {
        options.BaseServerUrl = baseServerUrl;
        return options;
    }

    /// <summary>
    /// Sets whether the base server URL should be dynamically determined based on the request context.
    /// </summary>
    /// <param name="options"><see cref="ScalarOptions" />.</param>
    /// <param name="dynamicBaseServerUrl">Whether to dynamically adjust the base server URL.</param>
    public static ScalarOptions WithDynamicBaseServerUrl(this ScalarOptions options, bool dynamicBaseServerUrl = true)
    {
        options.DynamicBaseServerUrl = dynamicBaseServerUrl;
        return options;
    }
}
