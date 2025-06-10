<script setup lang="ts">
import { computed } from 'vue'

defineProps<{
  isExpanded: boolean
}>()

const emit = defineEmits(['toggle'])

const min = defineModel('min', {
  type: Number as () => number | null,
  required: true
})

const max = defineModel('max', {
  type: Number as () => number | null,
  required: true
})

const formattedPriceRange = computed(() => {
  if (!min.value && !max.value) return null
  if (!max.value) return `${min.value}€ +`
  return `${min.value}€ - ${max.value}€`
})
</script>

<template>
  <div ref="componentRef" class="relative w-full bg-white rounded-3xl shadow-xl border border-gray-300">
    <CollapsedField
        label="Prix"
        :value="formattedPriceRange"
        default-value="Combien ?"
        :is-expanded="isExpanded"
        @toggle="emit('toggle')"
    >
      <div class="flex flex-col gap-4">
        <RangeSlider v-model:min="min" v-model:max="max"/>
        <PriceInput
            :prixMin="min"
            :prixMax="max"
            @update:prixMin="min = $event"
            @update:prixMax="max = $event"
        />
      </div>
    </CollapsedField>
  </div>
</template>