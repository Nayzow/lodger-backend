<script setup lang="ts">
import { computed } from 'vue'

interface Props {
  label: string
  value: string | number | null
  defaultValue: string
  placeholder?: string
  isExpanded: boolean
}

const props = defineProps<Props>()
const emit = defineEmits(['toggle'])

const displayValue = computed(() => {
  if (props.value === null || props.value === '') {
    return props.defaultValue
  }
  return props.value
})
</script>

<template>
  <div
      v-if="!isExpanded"
      @click="emit('toggle')"
      class="flex items-center justify-between w-full py-4 px-6 bg-transparent cursor-pointer"
  >
    <span class="text-lg font-semibold font-albert text-secondaire">{{ label }}</span>
    <span class="text-lg font-semibold font-albert text-secondaire">{{ displayValue }}</span>
  </div>
  <div
      v-else
      class="w-full bg-transparent flex flex-col py-4 px-6"
  >
    <div class="flex justify-between items-center mb-4">
      <span class="text-lg font-semibold font-albert text-secondaire">{{ label }}</span>
<!--      <button @click="emit('toggle')" class="text-secondaire">Fermer</button>-->
    </div>
    <slot/>
  </div>
</template>