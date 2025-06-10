<script setup lang="ts">
import type {SearchRequest} from "~/models/payloads/searchRequest";

defineModel<SearchRequest>('state', {
  type: Object as PropType<SearchRequest>,
  required: true,
});

defineProps({
  submit: Function as PropType<() => void>,
});
</script>

<template>
  <div
      class="hidden bg-white shadow-xl rounded-3xl border border-gray-300 px-3.5 py-1.5 lg:flex max-lg:flex-col lg:items-center gap-4 justify-between">
    <div class="flex items-center justify-center gap-4 lg:gap-8 flex-1">
      <PopoverLocationField v-model="state.localisation"/>
      <div class="h-10 border border-[#7E7E7E]"/>
      <PopoverTypeField v-model="state.type"/>
      <div class="h-10 border border-[#7E7E7E]"/>
      <PopoverPriceRangeField v-model:prixMin="state.prixMin" v-model:prixMax="state.prixMax"/>
    </div>
    <UButton size="md" class="bg-principal text-white rounded-2xl p-3" @click="submit && submit()">
      <UIcon name="i-custom-search" class="w-4 h-4"/>
    </UButton>
  </div>

</template>

<style scoped lang="scss">

</style>