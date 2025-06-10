<script setup lang="ts">

import type {InputSize} from "#ui/types";

const model = defineModel({
  type: String,
});

defineProps({
  size: {
    type: String as PropType<InputSize>,
    default: 'md',
  },
});

const oldSearchValues = ref<string[]>([]);

onMounted(() => {
  const searchValues = localStorage.getItem('previousSearches');
  if (searchValues) {
    oldSearchValues.value = JSON.parse(searchValues);
  }
});
</script>

<template>
  <div class="flex flex-col gap-2 text-secondaire max-w-sm">
    <label>Localisation</label>
    <LocationInput v-model="model" :size="size"/>
    <LastSearchValue v-for="value in oldSearchValues" :key="value" :last-search-value="value"
                     @click="model = value;"/>
  </div>
</template>

<style scoped lang="scss">

</style>