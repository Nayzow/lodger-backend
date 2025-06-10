<script setup lang="ts">

import type {Logement} from "~/models/entities/logement";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  },
  className: {
    type: String,
    default: 'hidden xl:block'
  }
})

function formatTitle(logement: Logement): string {
  const { ville, codePostal } = logement.adresse;
  const { nombreDePieces, superficie, meuble, etage, etageMax } = logement;

  const etageText = etage === 0 ? 'Rez-de-chaussée' : `Etage ${etage} / ${etageMax}`;
  const meubleText = meuble ? 'Meublé' : 'Non meublé';

  return `${ville} ${codePostal} - ${nombreDePieces} Pièces - ${superficie}m2 - ${meubleText} - ${etageText}`;
}
</script>

<template>
  <h2 class="text-lg md:text-xl lg:text-2xl text-gray-600">
    {{ formatTitle(logement) }}
  </h2>
</template>

<style scoped lang="scss">

</style>