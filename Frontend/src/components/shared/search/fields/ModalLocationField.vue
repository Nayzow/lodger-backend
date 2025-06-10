<script setup lang="ts">
import { ref, watch } from 'vue';

const isOpen = ref(false);

const model = defineModel<string>({
  type: String,
});

defineProps({
  submit: Function as PropType<() => void>,
  reset: Function as PropType<() => void>,
});

const { closeAllModalsEvent } = useModalCloser();

watch(closeAllModalsEvent, () => {
  isOpen.value = false;
});

const displayValue = computed(() => {
  if (!model.value) return 'Où ?';
  return model.value;
});


</script>

<template>
  <div
      @click="isOpen = true"
      class="flex items-center justify-between bg-white rounded-3xl shadow-xl border border-gray-300 w-full py-4 px-6 cursor-pointer"
  >
    <span class="text-lg font-semibold font-albert text-secondaire">Localisation</span>
    <span class="text-lg font-semibold font-albert text-secondaire">{{ displayValue }}</span>
  </div>
  <UModal fullscreen class="z-[9999999]" v-model="isOpen">
    <AdvancedLocationField v-model="model"
                           v-model:is-open="isOpen"
                           @update:model-value="model = $event"
                           :submit="submit"
                           :reset="reset"
    />
  </UModal>
</template>
