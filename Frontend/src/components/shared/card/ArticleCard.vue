<script setup lang="ts">

defineProps({
  title: {
    type: String,
    required: true
  },
  content: {
    type: String,
    required: true
  },
});
</script>

<template>
  <div class="flex flex-col justify-center gap-4 w-full rounded-xl bg-white shadow-md border border-gray-400 md:m-4 p-6">
    <div>
      <NuxtImg
          src="/images/hero-background.jpg"
          alt="hero background"
          class="w-full h-56 lg:h-[250px] object-cover object-center rounded-lg"
      />
    </div>
    <h2 class="max-w-2xl font-bold text-secondaire font-ninetea leading-tight md:leading-normal">
      {{ title }}
    </h2>
    <p class=" max-w-2xl text-gray-600">
      {{ content }}
    </p>
  </div>
</template>

<style scoped lang="scss">

</style>