namespace Scalar.AspNetCore.Extensions;

/// <summary>
/// Attribute to mark methods or classes to be excluded from the Scalar API reference.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Delegate)]
public sealed class ExcludeFromApiReferenceAttribute : Attribute;