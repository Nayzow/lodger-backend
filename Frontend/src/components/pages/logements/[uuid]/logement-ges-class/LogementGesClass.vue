<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";
import {gesClasses} from "~/utils/config/options/options";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  }
});


</script>

<template>
  <EnergyClassList :classes="gesClasses" :active-class="logement.ges" title="GES">
    <template #popover>
      <p class="text-sm max-w-xs text-justify text-secondaire p-4">
        Le GES est une mesure de l'impact environnemental de votre logement. Il est calculé en fonction de la quantité
        de gaz à effet de serre émise par votre logement.</p>
    </template>
  </EnergyClassList>
</template>

<style scoped lang="scss">

</style>