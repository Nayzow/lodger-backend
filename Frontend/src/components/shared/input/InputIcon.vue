<script setup lang="ts">

import {selectButtonUI} from "~/utils/config/ui";

defineProps({
  icon: {
    type: String,
    required: true,
  },
  value: {
    type: String,
    required: true,
  },
  label: {
    type: String,
    required: true,
  },
  model: {
    type: String,
    required: true,
  },
  className: {
    type: String,
    default: '',
  },
})

const router = useRouter();
const isSearchPage = computed(() => router.currentRoute.value.path === '/recherche');
const isHomePage = computed(() => router.currentRoute.value.path === '/');
</script>

<template>
  <UButton
      :ui="selectButtonUI"
      :icon="icon"
      size="lg"
      color="white"
      :class="{
        'bg-principal': model === value,
        'text-white': model === value,
        'rounded-full': isSearchPage,
        'py-4 px-6 flex-1': isHomePage,
      }"
      @click="$emit('click', value)"
  >
    {{ label }}
  </UButton>
</template>

<style scoped lang="scss">

</style>