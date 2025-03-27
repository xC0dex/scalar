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
}