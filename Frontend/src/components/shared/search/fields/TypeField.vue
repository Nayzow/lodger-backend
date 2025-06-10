<script setup lang="ts">

import type {PropertyType} from "~/models/types/propertyType";

const model = defineModel({
  type: String,
  required: true,
});

const {typeOptions} = defineProps({
  typeOptions: Array as PropType<{ icon: string, value: string, label: string }[]>,
  className: {
    type: String,
    default: 'max-w-sm flex-wrap',
  },
  buttonClassName: {
    type: String,
    default: '',
  },
});

onMounted(() => {
  if (!model.value) {
    model.value = 'Appartement'
  }
});
</script>

<template>
  <div class="flex gap-4 text-secondaire" :class="className">
    <template v-for="option in typeOptions"
              :key="option.value">
      <InputIcon
          :icon="option.icon"
          :value="option.value"
          :label="option.label"
          :model="model"
          @click="model = option.value as PropertyType"
      />
    </template>
  </div>
</template>

<style scoped lang="scss">

</style>