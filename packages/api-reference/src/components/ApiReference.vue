<script setup lang="ts">
import { apiReferenceConfigurationSchema } from '@scalar/types/api-reference'
import { useColorMode } from '@scalar/use-hooks/useColorMode'
import { useSeoMeta } from '@unhead/vue'
import { useFavicon } from '@vueuse/core'
import { computed, toRef, watch } from 'vue'

import { useReactiveSpec } from '@/hooks'
import type { ReferenceProps } from '@/types'

import { Layouts } from './Layouts'

const props = defineProps<ReferenceProps>()

// If content changes it is emitted to the parent component to be handed back in as a spec string
defineEmits<{
  (e: 'updateContent', value: string): void
}>()

const configuration = computed(() =>
  apiReferenceConfigurationSchema.parse(props.configuration),
)

const { toggleColorMode, isDarkMode } = useColorMode({
  initialColorMode: configuration.value.darkMode ? 'dark' : undefined,
  overrideColorMode: configuration.value.forceDarkModeState,
})

/** Update the dark mode state when props change */
watch(
  () => configuration.value.darkMode,
  (isDark) => (isDarkMode.value = !!isDark),
)

if (configuration.value?.metaData) {
  useSeoMeta(configuration.value.metaData)
}

const { parsedSpec, rawSpec } = useReactiveSpec({
  proxyUrl: toRef(() => configuration.value.proxyUrl || ''),
  specConfig: toRef(() => configuration.value.spec || {}),
})

const favicon = computed(() => configuration.value.favicon)
useFavicon(favicon)
</script>
<template>
  <!-- Inject any custom CSS directly into a style tag -->
  <component
    :is="'style'"
    v-if="configuration?.customCss">
    {{ configuration.customCss }}
  </component>
  <Layouts
    :configuration="configuration"
    :isDark="isDarkMode"
    :parsedSpec="parsedSpec"
    :rawSpec="rawSpec"
    @toggleDarkMode="() => toggleColorMode()"
    @updateContent="$emit('updateContent', $event)">
    <template #footer>
      <slot name="footer" />
    </template>
    <!-- Expose the content end slot as a slot for the footer -->
    <template #content-end>
      <slot name="footer" />
    </template>
  </Layouts>
</template>
<style>
@layer scalar-base {
  body {
    margin: 0;
    background-color: var(--scalar-background-1);
  }
}
</style>
