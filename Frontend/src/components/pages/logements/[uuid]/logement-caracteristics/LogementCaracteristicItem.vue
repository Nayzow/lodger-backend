<script setup lang="ts">

defineProps({
  icon: {
    type: String,
    default: ''
  },
  label: {
    type: String,
    default: ''
  }
});
</script>

<template>
  <div
      class="bg-transparent hover:bg-transparent text-black shadow-none cursor-default flex items-center gap-2 p-0"
  >
    <div class="flex items-center justify-center w-12 h-12 ">
      <UIcon :name="icon" class="font-bold w-8 h-8"/>
    </div>
    <p class="font-jakarta">{{ label }}</p>
  </div>
</template>

<style scoped lang="scss">

</style>