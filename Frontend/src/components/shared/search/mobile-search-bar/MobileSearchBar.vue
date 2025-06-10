<script setup lang="ts">
import type {SearchRequest} from "~/models/payloads/searchRequest";
import {typeOptions} from "~/utils/config/options/options";

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
      class="lg:hidden w-full bg-white shadow-md rounded-xl border border-gray-300 p-4 flex max-lg:flex-col lg:items-center gap-4 justify-between">
    <TypeField v-model="state.type" :type-options="typeOptions.slice(0, 2)" class-name="w-full max-w-initial"/>
    <LocationField v-model="state.localisation" size="xl"/>
    <PriceInput
        :prixMin="state.prixMin"
        :prixMax="state.prixMax"
        @update:prixMin="state.prixMin = $event"
        @update:prixMax="state.prixMax = $event"
        size="xl"
    />
    <UButton icon="material-symbols:search" size="xl" label="Rechercher" @click="submit && submit()"
             class="bg-secondaire justify-center py-4 px-6 text-white w-full rounded-xl"/>
  </div>
</template>

<style scoped lang="scss">

</style>