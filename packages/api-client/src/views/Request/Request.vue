<script setup lang="ts">
import type { RequestPayload } from '@scalar/oas-utils/entities/spec'
import { computed, ref } from 'vue'
import { useRouter } from 'vue-router'

import EmptyState from '@/components/EmptyState.vue'
import ImportCurlModal from '@/components/ImportCurl/ImportCurlModal.vue'
import ViewLayout from '@/components/ViewLayout/ViewLayout.vue'
import ViewLayoutContent from '@/components/ViewLayout/ViewLayoutContent.vue'
import { useLayout } from '@/hooks'
import { useSidebar } from '@/hooks/useSidebar'
import { importCurlCommand } from '@/libs/importers/curl'
import { PathId } from '@/routes'
import { useWorkspace } from '@/store'
import { useActiveEntities } from '@/store/active-entities'
import RequestSection from '@/views/Request/RequestSection/RequestSection.vue'
import RequestSubpageHeader from '@/views/Request/RequestSubpageHeader.vue'
import ResponseSection from '@/views/Request/ResponseSection/ResponseSection.vue'

defineEmits<(e: 'newTab', item: { name: string; uid: string }) => void>()

const { isSidebarOpen } = useSidebar()
const { layout } = useLayout()

const {
  activeCollection,
  activeExample,
  activeRequest,
  activeWorkspace,
  activeServer,
  activeEnvVariables,
  activeEnvironment,
  activeWorkspaceCollections,
  activeWorkspaceRequests,
} = useActiveEntities()

const workspaceContext = useWorkspace()
const { modalState, requestHistory, requestMutators, serverMutators, servers } =
  workspaceContext

// Extend the RequestPayload type to include url
type ExtendedRequestPayload = RequestPayload & {
  url?: string
}

const parsedCurl = ref<ExtendedRequestPayload>()
const selectedServerUid = ref('')
const router = useRouter()

const activeHistoryEntry = computed(() =>
  requestHistory.findLast((r) => r.request.uid === activeExample.value?.uid),
)

/**
 * Selected scheme UIDs
 *
 * In the modal we use collection.selectedSecuritySchemes and in the
 * standalone client we use request.selectedSecuritySchemeUids
 *
 * These are centralized here so they can be drilled down AND used in send-request
 */
const selectedSecuritySchemeUids = computed(
  () =>
    (layout === 'modal'
      ? activeCollection.value?.selectedSecuritySchemeUids
      : activeRequest.value?.selectedSecuritySchemeUids) ?? [],
)

function createRequestFromCurl({
  requestName,
  collectionUid,
}: {
  requestName: string
  collectionUid: string
}) {
  if (!parsedCurl.value) return

  const collection = activeWorkspaceCollections.value.find(
    (c) => c.uid === collectionUid,
  )

  if (!collection) return

  const isDrafts = collection?.info?.title === 'Drafts'

  // Prevent adding servers to drafts
  if (!isDrafts && parsedCurl.value.servers) {
    // Find existing server to avoid duplication
    const existingServer = Object.values(servers).find(
      (s) => s.url === parsedCurl?.value?.servers?.[0],
    )
    if (existingServer) {
      selectedServerUid.value = existingServer.uid
    } else {
      selectedServerUid.value = serverMutators.add(
        { url: parsedCurl.value.servers[0] ?? '/' },
        collection.uid,
      ).uid
    }
  }

  // Add the request and use the url if it's a draft as a path
  const newRequest = requestMutators.add(
    {
      summary: requestName,
      path: isDrafts ? parsedCurl?.value?.url : parsedCurl?.value?.path,
      method: parsedCurl?.value?.method,
      parameters: parsedCurl?.value?.parameters,
      selectedServerUid: isDrafts ? undefined : selectedServerUid.value,
      requestBody: parsedCurl?.value?.requestBody,
    },
    collection.uid,
  )

  if (newRequest && activeWorkspace.value?.uid) {
    router.push({
      name: 'request',
      params: {
        [PathId.Workspace]: activeWorkspace.value.uid,
        [PathId.Collection]: collection.uid,
        [PathId.Request]: newRequest.uid,
      },
    })
  }
  modalState.hide()
}

function handleCurlImport(curl: string) {
  parsedCurl.value = importCurlCommand(curl)
  modalState.show()
}
</script>

<template>
  <div
    v-if="activeCollection && activeWorkspace"
    class="bg-b-1 relative z-0 flex h-full flex-1 flex-col overflow-hidden pt-0"
    :class="{
      '!mb-0 !mr-0 !border-0': layout === 'modal',
    }">
    <div class="flex h-full">
      <!-- Ensure we have a request for this view -->
      <div
        v-if="activeRequest"
        class="flex h-full flex-1 flex-col">
        <RequestSubpageHeader
          v-model="isSidebarOpen"
          :collection="activeCollection"
          :envVariables="activeEnvVariables"
          :environment="activeEnvironment"
          :operation="activeRequest"
          :server="activeServer"
          :workspace="activeWorkspace"
          @hideModal="() => modalState.hide()"
          @importCurl="handleCurlImport" />
        <ViewLayout>
          <!-- TODO possible loading state -->
          <ViewLayoutContent
            v-if="activeExample"
            class="flex-1"
            :class="[isSidebarOpen ? 'sidebar-active-hide-layout' : '']">
            <RequestSection
              :collection="activeCollection"
              :envVariables="activeEnvVariables"
              :environment="activeEnvironment"
              :example="activeExample"
              :operation="activeRequest"
              :selectedSecuritySchemeUids="selectedSecuritySchemeUids"
              :server="activeServer"
              :workspace="activeWorkspace" />
            <ResponseSection
              :numWorkspaceRequests="activeWorkspaceRequests.length"
              :response="activeHistoryEntry?.response" />
          </ViewLayoutContent>
        </ViewLayout>
      </div>

      <!-- No active request -->
      <EmptyState v-else />
    </div>
  </div>

  <!-- No Collection or Workspace -->
  <EmptyState v-else />

  <ImportCurlModal
    v-if="parsedCurl"
    :collectionUid="activeCollection?.uid ?? ''"
    :parsedCurl="parsedCurl"
    :state="modalState"
    @close="modalState.hide()"
    @importCurl="createRequestFromCurl" />
</template>

<style scoped>
.request-text-color-text {
  color: var(--scalar-color-1);
  background: linear-gradient(
    var(--scalar-background-1),
    var(--scalar-background-3)
  );
  box-shadow: 0 0 0 1px var(--scalar-border-color);
}
@media screen and (max-width: 800px) {
  .sidebar-active-hide-layout {
    display: none;
  }
  .sidebar-active-width {
    width: 100%;
  }
}
</style>
