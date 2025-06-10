<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";
import {energyClasses} from "~/utils/config/options/options";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  }
});


</script>

<template>
  <EnergyClassList :classes="energyClasses" :active-class="logement.classeEnergie" title="Classe énergie">
    <template #popover>
      <p class="text-sm max-w-xs text-justify text-secondaire p-4">
        La classe énergie est une indication de la consommation énergétique du logement.
        Elle est calculée en fonction de la consommation d'énergie annuelle du logement.</p>
    </template>
  </EnergyClassList>
</template>

<style scoped lang="scss">

</style>