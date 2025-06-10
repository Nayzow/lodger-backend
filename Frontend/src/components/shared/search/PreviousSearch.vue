<script setup lang="ts">
import {ref, onMounted} from 'vue'

const props = defineProps({
  onSelect: {
    type: Function,
    required: true
  },
  isSearchPage: {
    type: Boolean,
    default: false
  }
})


const previousSearches = ref<string[]>([])

const loadPreviousSearches = () => {
  const storedSearches = localStorage.getItem('previousSearches')
  if (storedSearches) {
    previousSearches.value = JSON.parse(storedSearches)
  }
}

const selectSearch = (search: string) => {
  props.onSelect(search)
}

const clearPreviousSearches = () => {
  localStorage.removeItem('previousSearches')
  previousSearches.value = []
}

const clearPreviousSearch = (search: string) => {
  previousSearches.value = previousSearches.value.filter((s) => s !== search)
  localStorage.setItem('previousSearches', JSON.stringify(previousSearches.value))
}
onMounted(loadPreviousSearches)
</script>

<template>
  <div v-if="previousSearches.length > 0" class="mt-4">
    <div class="flex gap-4 items-center mb-2">
      <h3 class="text-lg font-semibold">Recherches précédentes</h3>
      <UButton
          variant="ghost"
          icon="i-heroicons-x-mark"
          size="sm"
          @click="clearPreviousSearches"
          class="text-gray-500 hover:text-gray-700"
      />
    </div>
    <div class="flex max-md:flex-col flex-wrap max-w-md gap-2">
      <div v-for="search in previousSearches"
           :key="search"
           class="flex items-center gap-4 p-2  hover:bg-gray-200 cursor-pointer"
           :class="{'rounded-full bg-principal w-fit text-white px-4 py-2 text-sm': isSearchPage}"
      >
        <LastSearchValue
            :lastSearchValue="search"
            @click="selectSearch(search)"
            :show-icon="false"
            class-name="text-xs"
        />
        <UIcon name="i-heroicons-x-mark" class="h-4 w-4"
               :class="{ 'text-white': isSearchPage,
                       'text-gray-500 hover:text-gray-700': !isSearchPage
                     }"
               @click="clearPreviousSearch(search)"/>
      </div>
    </div>

  </div>
</template>