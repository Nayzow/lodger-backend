<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";

defineProps({
  logements: {
    type: Array as PropType<Logement[]>,
    required: true,
  },
  ui: {
    type: Object as PropType<Record<string, string>>,
    default: () => ({}),
  },
});
</script>

<template>
  <UCarousel
      :items="logements"
      arrows
      :ui="ui"
  >
    <template #default="{ item }">
      <LogementCard :logement="item" class-name="m-2 lg:max-w-[350px]" :show-arrows="false"/>
    </template>
    <template #prev="{ onClick, disabled}">
      <button
          @click="onClick"
          type="button"
          class="opacity-0 group-hover/section:opacity-100 transition-all flex items-center justify-center rounded-xl border border-gray-300 bg-white text-secondaire hover:bg-white hover:text-secondaire disabled:bg-white absolute top-1/2 -left-3 transform -translate-y-1/2 p-2"
          :disabled="disabled"
      >
        <UIcon name="material-symbols:chevron-left" class="w-6 h-6"/>
      </button>
    </template>
    <template #next="{ onClick, disabled}">
      <button
          @click="onClick"
          type="button"
          class="opacity-0 group-hover/section:opacity-100 transition-all flex items-center justify-center rounded-xl border border-gray-300 bg-white text-secondaire hover:bg-white hover:text-secondaire disabled:bg-white absolute top-1/2 -right-3 transform -translate-y-1/2 px-2.5 py-2"
          :disabled="disabled"
      >
        <UIcon name="material-symbols:chevron-right" class="w-6 h-6"/>
      </button>
    </template>
  </UCarousel>
</template>

<style scoped lang="scss">
button {
  transition: opacity 0.3s ease-in-out;
}
</style>