<script setup lang="ts">
import type { Logement } from "~/models/entities/logement";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  }
});

const logementInfos = [
  {
    icon: "i-custom-bed",
    value: (logement: Logement) => logement.chambre,
  },
  {
    icon: "i-custom-sdb",
    value: (logement: Logement) => logement.sdo,
  },
  {
    icon: "i-custom-car",
    value: (logement: Logement) => logement.garage,
  },
  {
    icon: "i-custom-superficie",
    value: (logement: Logement) => `${logement.superficie}m²`,
  },
  {
    icon: "i-custom-sofa",
    value: (logement: Logement) => logement.meuble ? 'Meublé' : 'Non meublé',
  }
];
</script>

<template>
  <div class="order-3 xl:order-2 flex justify-between items-stretch gap-4 text-secondaire bg-gray-100 font-semibold rounded-lg p-4">
    <div
        v-for="(info, index) in logementInfos"
        :key="index"
        class="flex flex-col items-center justify-start flex-1 bg-transparent gap-2 hover:bg-transparent text-secondaire font-ninetea p-0 cursor-default shadow-none"
    >
      <UIcon :name="info.icon" size="md" class="text-secondaire h-6 w-6 mb-2"/>
      <span class="text-center text-sm leading-tight">{{ info.value(logement) }}</span>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>

