<script setup lang="ts">
import type { PropertyType } from "~/models/types/propertyType";
import { typeOptions } from "~/utils/config/options/options";

const props = defineProps<{
  isExpanded: boolean
}>()

const emit = defineEmits(['toggle'])

const model = defineModel({
  type: String,
  required: true,
});
</script>

<template>
  <div ref="componentRef" class="relative w-full bg-white rounded-3xl shadow-xl border border-gray-300">
    <CollapsedField
        label="Type de bien"
        :value="model"
        default-value="Quoi ?"
        :is-expanded="isExpanded"
        @toggle="emit('toggle')"
    >
      <TypeField v-model:model-value="model" :type-options="typeOptions"/>
    </CollapsedField>
  </div>
</template>