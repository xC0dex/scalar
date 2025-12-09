import type { NewChangesetWithCommit } from '@changesets/types'

import type { GitHubInfo } from './types'

/**
 * Formats a release line for a single change
 * Format: - [#PR_NUMBER](link): description
 * WITHOUT commit hash, WITHOUT "Thanks @user"
 */
export function formatReleaseLine(changeset: NewChangesetWithCommit, githubInfo: GitHubInfo | null): string {
  const description = changeset.summary.trim()

  const prLink = getPRLink(githubInfo)
  if (prLink) {
    return `- ${prLink}: ${description}`
  }

  // Fallback if no PR link available
  return `- ${description}`
}

/**
 * Checks if the text already contains a markdown PR link (e.g. "[#123](https://github.com/scalar/scalar/pull/123)")
 * Pattern: [#\d+](...)
 */
function containsPRLink(text: string): boolean {
  const prLinkPattern = /^\[#\d+\].*/
  return prLinkPattern.test(text.trim())
}

/**
 * Extracts PR link from GitHub info
 * Returns format: [#PR_NUMBER](link) or null
 */
export function getPRLink(githubInfo: GitHubInfo | null): string | null {
  if (!githubInfo || !githubInfo.links.pull) {
    return null
  }

  if (containsPRLink(githubInfo.links.pull)) {
    return githubInfo.links.pull
  }

  return `[#${githubInfo.pull}](${githubInfo.links.pull})`
}

/**
 * Formats a dependency section header
 * Format: - **@scalar/package-name@version**
 */
export function formatDependencyHeader(packageName: string, version: string): string {
  return `- **${packageName}@${version}**`
}

/**
 * Formats a dependency change line
 * Format:   - [#PR_NUMBER](link): description
 */
export function formatDependencyChange(githubInfo: GitHubInfo | null, description: string): string {
  const trimmedDescription = description.trim()

  // If description already contains a PR link, use it as-is
  if (containsPRLink(trimmedDescription)) {
    return `  - ${trimmedDescription}`
  }

  // Otherwise, add PR link if available
  const prLink = getPRLink(githubInfo)
  if (prLink) {
    return `  - ${prLink}: ${trimmedDescription}`
  }

  // Fallback if no PR link available
  return `  - ${trimmedDescription}`
}
