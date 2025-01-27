import { http11 } from '@/httpsnippet-lite/esm/targets/http/http1.1/client'
import type { Plugin } from '@/types'
import { convertWithHttpSnippetLite } from '@/utils/convertWithHttpSnippetLite'

/**
 * http/http1.1
 */
export const httpHttp11: Plugin = {
  target: 'http',
  client: 'http1.1',
  title: 'HTTP/1.1',
  generate(request) {
    // TODO: Write an own converter
    return convertWithHttpSnippetLite(http11, request)
  },
}
