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
  <div
      class="fixed bottom-0 left-0 right-0 z-40 flex justify-between gap-4 bg-slate-50 p-4 border xl:static xl:bg-inherit xl:border-none xl:p-0 xl:shadow-none">
    <div class="flex flex-col gap-2">
      <p class="text-xl xl:text-[27.5px] text-principal font-bold font-albert">{{ logement.loyer }}€/mois</p>
      <p class="text-md xl:text-xl text-secondaire font-semibold font-albert">{{ logement.charge }} € de charges</p>
    </div>
    <LogementOverviewButton class-name="xl:hidden flex items-center"/>
  </div>

</template>

<style scoped lang="scss">

</style>