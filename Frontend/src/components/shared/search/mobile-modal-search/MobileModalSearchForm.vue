<script setup lang="ts">
import type { SearchRequest } from "~/models/payloads/searchRequest";

const { setExpandedField, isFieldExpanded } = useExpandedField()

defineModel<SearchRequest>('state', {
  type: Object as PropType<SearchRequest>,
  required: true,
});

defineProps({
  reset: Function as PropType<() => void>,
  submit: Function as PropType<() => void>,
});
</script>

<template>
  <div class="flex-1 lg:hidden h-full w-full flex flex-col justify-between gap-4">
    <div class="flex flex-col gap-4">
      <ModalLocationField v-model="state.localisation" :submit="submit" :reset="reset"/>
      <CollapsedPriceRangeField
          v-model:min="state.prixMin"
          v-model:max="state.prixMax"
          :is-expanded="isFieldExpanded('price')"
          @toggle="setExpandedField(isFieldExpanded('price') ? null : 'price')"
      />
      <CollapsedTypeField
          v-model:model-value="state.type"
          :is-expanded="isFieldExpanded('type')"
          @toggle="setExpandedField(isFieldExpanded('type') ? null : 'type')"
      />
    </div>
    <div class="flex gap-4">
      <UButton variant="ghost" class="flex-1 justify-center text-gray-700 underline hover:bg-white" @click="reset">Tout effacer</UButton>
      <UButton icon="material-symbols:search" size="lg" label="Rechercher" @click="submit && submit()"
               class="flex-1 bg-secondaire justify-center text-white rounded-xl"/>
    </div>
  </div>
</template>