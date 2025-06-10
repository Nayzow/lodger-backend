<script setup lang="ts">

import {buttonUI} from "~/utils/config/ui";

defineProps({
  title: {
    type: String,
    required: true,
  }
});
</script>

<template>
  <section class="group/section flex flex-col gap-4">
    <div class="flex justify-between items-center gap-4">
      <h1 class="mx-4">
        {{ title }}
      </h1>
      <div class="flex gap-2">
        <UButton
            to="/logements"
            variant="link"
            :ui="buttonUI"
            color="white"
            class="underline font-ninetea text-secondaire text-xs md:text-sm lg:text-lg"
        >
          Voir plus
        </UButton>
      </div>
    </div>
    <slot/>
  </section>
</template>

<style scoped lang="scss">

</style>