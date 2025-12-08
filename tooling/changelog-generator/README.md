# @scalar-internal/changelog-generator

Custom changelog generator for Scalar packages that creates cleaner, more readable changelogs.

## Features

- **No commit hashes**: Changelogs only show PR links, not commit hashes
- **No "Thanks" messages**: Cleaner format without contributor attribution in changelog
- **Better structure**: Package changes appear first, followed by dependency updates
- **Dependency details**: Each dependency update shows its individual changes with PR links

## Format

### Package Changes

```markdown
## 2.11.1

### Patch Changes
- [#7465](https://github.com/scalar/scalar/pull/7465): fix description
- [#7460](https://github.com/scalar/scalar/pull/7460): feat description
```

### Dependency Updates

```markdown
### Updated Dependencies

- **@scalar/api-reference@1.40.1**
  - [#7465](https://github.com/scalar/scalar/pull/7465): ensure workspace-store consumers rely only on public exports
  - [#7460](https://github.com/scalar/scalar/pull/7460): update and polish ScalarLoading

- **@scalar/dotnet-shared@0.1.1**
  - [#7123](https://github.com/scalar/scalar/pull/7123): add new utility functions
```

## Usage

This package is automatically used by Changesets when configured in `.changeset/config.json`:

```json
{
  "changelog": [
    "@scalar-internal/changelog-generator",
    {
      "repo": "scalar/scalar"
    }
  ]
}
```

## Development

```bash
# Build the package
pnpm build

# Type check
pnpm types:check
```
