<script setup lang="ts">
import {iconButtonUI} from "~/utils/config/ui";
import type {Logement} from "~/models/entities/logement";

defineProps({
  logement: {
    type: Object as PropType<Logement>,
    required: true
  }
})

</script>

<template>
  <article class="lg:h-64 bg-white border rounded-lg shadow-md flex flex-col lg:flex-row gap-2 text-xl">
    <UCarousel
        v-slot="{ item }"
        :items="logement.photos"
        :ui="{ item: 'basis-full' }"
        class="overflow-hidden lg:w-[250px]"
        arrows
    >
        <NuxtImg class="object-cover w-full h-64 rounded-t-lg lg:rounded-tr-none lg:rounded-l-lg"
                 :src="item.url"
                 alt="Image de l'annonce"/>
    </UCarousel>
    <NuxtLink
        :to="{name: 'logements-uuid', params: {uuid: logement.uuid}}"
        class="flex-1 flex flex-col p-6 gap-2"
    >
      <div class="flex justify-between items-center">
        <p class="text-gray-400 font-bold"><span class="text-secondaire text-3xl">{{ logement.loyer }}€</span> / mois</p>
        <UButton
            type="button"
            icon="material-symbols:favorite-rounded"
            class="bg-white hover:bg-principal text-gray-400 hover:text-white rounded-md p-2 border shadow"
            aria-label="Ajouter aux favoris"
            :ui="iconButtonUI"
        />
      </div>
      <h2 class="text-secondaire font-bold">
        {{ logement.type }} {{ logement.adresse.quartier ?? '' }}
      </h2>
      <p class="text-gray-400">{{ logement.adresse.nomRue}}, {{ logement.adresse.ville }} {{ logement.adresse.codePostal }}</p>
      <div class="flex flex-wrap py-2 gap-2">
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          {{ logement.nombreDePieces }} pièces
        </span>
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          {{ logement.chambre }} chambre
        </span>
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          {{ logement.sdo }} salle de bain
        </span>
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          {{ logement.superficie }}m²
        </span>
      </div>
    </NuxtLink>
  </article>
</template>

