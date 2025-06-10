<script setup lang="ts">
import {inputUI} from "~/utils/config/ui";
import type {InputSize} from "#ui/types";

defineProps({
  size: {
    type: String as PropType<InputSize>,
    default: 'lg',
  },
})
const emits = defineEmits(['update:prixMin', 'update:prixMax']);


const prixMin = defineModel('prixMin', {
  type: Number as () => number | null,
  required: true
})

const prixMax = defineModel('prixMax', {
  type: Number as () => number | null,
  required: true
})

const min = computed({
  get: () => prixMin.value ?? "",
  set: (value: number) => emits('update:prixMin', value),
});

const max = computed({
  get: () => prixMax.value ?? "",
  set: (value: number) => emits('update:prixMax', value),
});

const updatePrixMin = (value: number) => {
  emits('update:prixMin', value);
};

const updatePrixMax = (value: number) => {
  emits('update:prixMax', value);
};
</script>

<template>
  <div class="flex gap-4 max-w-xs">
    <div class="flex flex-col gap-2">
      <label class="text-secondaire font-bold">Prix min</label>
      <UInput
          :model-value="min"
          @update:model-value="updatePrixMin"
          :ui="inputUI"
          icon="material-symbols:euro"
          class="text-principal"
          type="number"
          :size="size"
      />
    </div>
    <div class="flex flex-col gap-2">
      <label class="text-secondaire font-bold">Prix max</label>
      <UInput
          :model-value="max"
          @update:model-value="updatePrixMax"
          :ui="inputUI"
          icon="material-symbols:euro"
          class="text-principal"
          type="number"
          :size="size"
      />
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>