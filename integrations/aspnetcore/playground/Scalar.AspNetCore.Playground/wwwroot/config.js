export default {
  generateOperationSlug: (operation) => `${operation.method.toLowerCase()}${operation.path}`,
}
