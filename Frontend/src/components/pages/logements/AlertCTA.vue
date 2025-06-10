<script setup lang="ts">
import {buttonUI} from "~/utils/config/ui";

const props = defineProps({
  class: String,
})
</script>

<template>
  <div class="z-30 bg-secondaire text-white p-2 flex max-lg:flex-col justify-center items-center gap-4"
       :class="props.class">
    <p class="font-ninetea text-white">
      Soyez <span class="text-principal font-bold">alerté</span> quand des <span class="text-principal font-bold">nouvelles annonces</span>
      sont <span class="text-principal font-bold">disponibles</span> pour votre recherche !
    </p>
    <div class="flex justify-between items-center w-full lg:w-auto gap-4">
      <UButton label="Activer l'alerte"
               size="sm"
               color="white"
               :ui="buttonUI"
               class="rounded-full px-6 py-1.5"/>
      <UIcon name="i-custom-lodger"
             class="lg:hidden text-secondaire w-8 h-8"/>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>