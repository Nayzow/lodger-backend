<script setup lang="ts">
defineProps({
  lastSearchValue: {
    type: String,
    required: true
  },
  showIcon: {
    type: Boolean,
    default: true
  },
  className: {
    type: String,
    default: ''
  }
});

</script>

<template>
  <p class="flex items-center gap-2  cursor-pointer" :class="className">
    <UIcon name="material-symbols:rotate-left" class="font-semibold" v-if="showIcon" />
    <span>{{ lastSearchValue }}</span>
  </p>
</template>

<style scoped lang="scss">

</style>