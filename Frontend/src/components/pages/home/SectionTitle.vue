<script setup lang="ts">
defineProps({
  className: {
    type: String,
    required: false,
    default: ''
  },
  title: {
    type: String,
    required: true
  },
  description: {
    type: String,
    required: true
  }
});
</script>

<template>
  <div class="flex flex-col gap-6 md:mx-4" :class="className">
    <UDivider v-if="$device.isMobile" size="xs" class="w-20" :ui="{border:{ base: 'border-secondaire'}}"/>
    <h2>{{ title }}</h2>
    <p class="font-jakarta text-gray-500 max-w-lg">{{ description }}</p>
  </div>
</template>

<style scoped lang="scss">

</style>