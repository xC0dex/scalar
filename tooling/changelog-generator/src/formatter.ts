import type { NewChangesetWithCommit } from './types.js'

export interface GitHubInfo {
  user: string | null
  pull: number | null
  links: {
    commit: string
    pull: string | null
    user: string | null
  }
}

/**
 * Formats a release line for a single change
 * Format: - [#PR_NUMBER](link): description
 * WITHOUT commit hash, WITHOUT "Thanks @user"
 */
export function formatReleaseLine(changeset: NewChangesetWithCommit, githubInfo: GitHubInfo | null): string {
  const description = changeset.summary.trim()

  // If summary already contains a PR link, use it as-is
  if (hasPRLinkInSummary(description)) {
    return `- ${description}`
  }

  // Otherwise, add PR link if available
  const prLink = getPRLink(githubInfo)
  if (prLink) {
    return `- ${prLink}: ${description}`
  }

  // Fallback if no PR link available
  return `- ${description}`
}

/**
 * Checks if the summary already contains a markdown PR link
 * Pattern: [#\d+](...)
 */
export function hasPRLinkInSummary(summary: string): boolean {
  // Match markdown link pattern like [#7](https://github.com/) at the start
  const prLinkPattern = /^\[#\d+\].*/
  return prLinkPattern.test(summary.trim())
}

/**
 * Extracts PR link from GitHub info
 * Returns format: [#PR_NUMBER](link) or null
 */
export function getPRLink(githubInfo: GitHubInfo | null): string | null {
  if (!githubInfo || !githubInfo.pull || !githubInfo.links.pull) {
    return null
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
  if (hasPRLinkInSummary(trimmedDescription)) {
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
