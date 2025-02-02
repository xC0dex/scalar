/**
 * Gets the base path by removing the specified suffix from the end of the current path.
 * @param {string} suffix - The suffix to remove from the end of the path.
 * @returns {string} The base path without the suffix.
 */
// eslint-disable-next-line
function getBasePath(suffix) {
  const path = window.location.pathname
  if (path.endsWith(suffix)) {
    return path.slice(0, -suffix.length)
  }
  return path
}

/**
 * Constructs a relative server URL from the given base path.
 * @param {string} basePath - The base path to convert to a relative server URL.
 * @returns {string} The relative server URL.
 */
// eslint-disable-next-line
function getRelativeServerUrl(basePath) {
  if (basePath.startsWith('/')) return basePath
  else return `/${basePath}`
}
