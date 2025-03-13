/**
 * Extracts the base path by removing the specified suffix from the current path.
 * @param {string} suffix - The suffix to remove from the end of the path.
 * @returns {string} The base path without the suffix.
 */
const getBasePath = (suffix) => {
  const path = window.location.pathname
  if (path.endsWith(suffix)) {
    return path.slice(0, -suffix.length)
  }
  return path
}

/**
 * Initializes the API reference.
 * @param {string} path - The current request path.
 * @param {boolean} isOpenApiRoutePatternUrl - Indicates whether OpenAPI URLs are absolute paths.
 * @param {boolean} useDynamicBaseServerUrl - Indicates whether the baseServerURL should be dynamically set.
 * @param {Object} configuration - The Scalar API reference configuration.
 */
const initialize = (path, isOpenApiRoutePatternUrl, useDynamicBaseServerUrl, configuration) => {
  const basePath = getBasePath(path)
  if (!isOpenApiRoutePatternUrl) {
    // Support for subdirectory hosting
    configuration.sources.forEach((source) => (source.url = `${window.location.origin}${basePath}/${source.url}`))
  }
  if (useDynamicBaseServerUrl) {
    configuration.baseServerURL = `${window.location.origin}${basePath}`
  }

  Scalar.createApiReference('#app', configuration)
}
