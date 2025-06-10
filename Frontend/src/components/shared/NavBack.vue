<script setup lang="ts">

import { useRouter } from 'vue-router';

const router = useRouter();

</script>

<template>
  <div class="px-4 lg:px-24 relative">
    <UButton
        icon="i-heroicons-arrow-left"
        size="lg"
        class="xl:hidden bg-white text-secondaire hover:text-white w-fit rounded-full shadow-md border mb-6"
        @click="router.back()"
    />
    <MobileDashboardBottomNav/>
  </div>
</template>

<style scoped lang="scss">

</style>