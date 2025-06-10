<script setup lang="ts">

defineProps({
  title: {
    type: String,
    required: true
  },
  icon: {
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
  <div class="group bg-white hover:bg-secondaire hover:translate-y-2 transition-all duration-700 shadow-md rounded-xl p-8 flex gap-3 w-full 4xl:w-[500px]">
    <Icon :name="icon" class="w-10 h-12 md:w-12 md:h-12"/>
    <div class="flex-1 flex flex-col font-jakarta gap-4">
      <h3 class="group-hover:text-white">{{ title }}</h3>
      <p class="text-secondaire group-hover:text-white">{{ description }}</p>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>