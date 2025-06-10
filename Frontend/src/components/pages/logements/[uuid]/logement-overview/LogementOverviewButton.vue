<script setup lang="ts">

import {buttonUI} from "~/utils/config/ui";

defineProps({
  className: {
    type: String,
    default: ''
  }
});
</script>

<template>
  <div class="order-4" :class="className">
    <UButton
        color="white"
        size="lg"
        class="w-auto xl:w-full xl:flex xl:justify-center text-md xl:text-xl font-ninetea"
        :ui="buttonUI"
    >
      Faire une demande
    </UButton>
  </div>
</template>

<style scoped lang="scss">

</style>