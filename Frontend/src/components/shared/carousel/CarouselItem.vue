<script setup lang="ts">

import {iconButtonUI} from "~/utils/config/ui";

const fakeArray = Array.from({length: 3}, (_, i) => i);

</script>

<template>
  <article class="relative bg-white shadow-md border rounded-lg flex flex-col w-72 gap-2">
    <UCarousel
        v-slot="{ item }"
        :items="fakeArray"
        :ui="{ item: 'basis-full' }"
        class="overflow-hidden"
        arrows
    >
      <div class="group w-full">
        <NuxtImg class="object-cover w-full h-44 rounded-t-lg"
                 src="https://media.tacdn.com/media/attractions-content--1x-1/0f/54/e8/a6.jpg"
                 alt="Image de l'annonce"/>

      </div>
    </UCarousel>
    <UButton
        type="button"
        icon="material-symbols:favorite-rounded"
        class="absolute top-2 right-2 bg-white hover:bg-white/80 text-gray-400 rounded-md p-2 shadow-md"
        aria-label="Ajouter aux favoris"
        :ui="iconButtonUI"
    />
    <NuxtLink to="/logements/1" class="flex-1 flex flex-col font-jakarta p-6 gap-2">
      <div class="flex justify-between items-center">
        <p class="text-gray-400 font-bold"><span class="text-secondaire text-3xl">755€</span> / mois <span
            class="text-xs">cc</span></p>
      </div>
      <h2 class="text-secondaire font-bold">Appartement 2 pièces 45m²</h2>
      <p class="text-gray-400">Paris 75018</p>
      <div class="flex flex-wrap py-2 font-ninetea gap-2">
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          2 pièces
        </span>
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          1 chambre
        </span>
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          1 salle de bain
        </span>
        <span class="bg-gray-100 text-secondaire text-md font-medium me-2 px-2.5 py-0.5 rounded">
          23m²
        </span>
      </div>
    </NuxtLink>
  </article>
</template>