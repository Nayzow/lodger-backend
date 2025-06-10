<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  }
});

</script>

<template>
  <div class="flex flex-col font-jakarta gap-8">
    <h3 class="text-md lg:text-xl 2xl:text-2xl font-bold font-albert text-secondaire">Conditions financières</h3>
    <div class="flex flex-row flex-wrap justify-between gap-y-4">
      <div class="w-1/2 lg:w-auto order-3 lg:order-1 flex flex-col gap-4">
        <h4 class="text-lg font-semibold">Loyer charges comprises</h4>
        <p class="text-lg">{{ logement.loyer + logement.charge }}€/ mois</p>
      </div>
      <div class="w-1/2 lg:w-auto order-2 flex flex-col gap-4">
        <h4 class="flex-1 text-lg font-semibold">Dont charges</h4>
        <p class="text-lg">{{ logement.charge }}€ / mois</p>
      </div>
      <div class="w-1/2 lg:w-auto order-1 lg:order-3 flex flex-col gap-4">
        <h4 class="text-lg font-semibold">Dépôt de garantie</h4>
        <p class="text-lg">{{ logement.garantie }}€</p>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>