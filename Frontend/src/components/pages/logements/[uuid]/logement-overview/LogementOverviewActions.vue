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
  <div class="order-1 xl:order-3 flex flex-col gap-3 lg:gap-6 my-2">
    <UButton
        :to="`https://www.google.com/maps/search/?api=1&query=${logement.adresse.ville} ${logement.adresse.quartier}`"
        target="_blank"
        variant="link"
        size="md"
        class="font-jakarta  font-medium text-secondaire p-0 text-md"
    >
      <UIcon name="clarity:map-marker-line" class="w-5 h-5"/>
      {{ logement.adresse.ville }}, {{ logement.adresse.quartier }}
    </UButton>
    <UButton
        variant="link"
        to="/#"
        size="md"
        class="underline font-jakarta font-medium text-secondaire p-0"
    >
      <UIcon name="i-custom-loti" class="w-5 h-5 text-orange-500"/>
      Voir les avis sur Lot
    </UButton>
    <UButton
        variant="link"
        to="/#"
        size="md"
        class="underline font-jakarta font-medium text-secondaire p-0"
    >
      <UIcon name="iconamoon:invoice" class="w-5 h-5"/>
      Aperçu des paiements
    </UButton>
  </div>
</template>

<style scoped lang="scss">

</style>