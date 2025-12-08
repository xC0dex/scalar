import type {
  ChangelogFunctions,
  ModCompWithPackage,
  NewChangesetWithCommit,
} from '@changesets/types'

export type { ChangelogFunctions, ModCompWithPackage, NewChangesetWithCommit }

export interface ChangelogOptions {
  repo: string
}
