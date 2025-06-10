<script setup lang="ts">

import {basicSearchFormGroupUI, popoverUI} from "~/utils/config/ui";

const {label, customLabel, value} = defineProps({
  label: {
    type: String,
    required: true
  },
  customLabel: {
    type: String,
    required: false
  },
  value: {
    type: [String, Number],
  },
});
</script>

<template>
  <div class="relative z-50 flex flex-col justify-center max-w-[120px]">
    <p class="text-sm text-gray-500">{{ label }}</p>
    <UPopover :ui="popoverUI">
      <UButton size="md" variant="ghost"
               class="text-secondaire text-lg font-bold p-0 hover:bg-transparent">
        {{ value }}
      </UButton>
      <template #panel>
        <UFormGroup :label="customLabel ?? label" :ui="basicSearchFormGroupUI">
          <slot/>
        </UFormGroup>
      </template>
    </UPopover>
  </div>
</template>

<style scoped lang="scss">

</style>