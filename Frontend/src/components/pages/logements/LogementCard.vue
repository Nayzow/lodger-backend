<script setup lang="ts">
import type { Logement } from "~/models/entities/logement";

const props = defineProps({
  logement: {
    type: Object as PropType<Logement>,
    required: true
  },
  className: {
    type: String,
    default: ''
  },
  showArrows: {
    type: Boolean,
    default: true
  },
});

const navigateToDetails = () => {
  navigateTo({ name: 'logements-uuid', params: { uuid: props.logement.uuid } });
};
</script>

<template>
  <div
      class="relative font-albert group/card bg-white border border-slate-300 rounded-xl flex flex-col gap-2 text-lg overflow-x-hidden"
      :class="className"
  >
    <UCarousel
        :items="logement.photos"
        :ui="{
        item: 'basis-full',
        indicators: {
          wrapper: 'absolute bottom-2 left-1/2 transform -translate-x-1/2 flex space-x-1 gap-0'}
      }"
        indicators
        arrows
        class="overflow-hidden"
    >
      <template #indicator="{ onClick, page, active }">
        <button
            v-if="page <= 5"
            @click="onClick"
            :class="['w-1 h-1 rounded-full p-1', active || page > 5 ? 'bg-primary' : ' bg-gray-400']"
        />
        <div v-else class="hidden"/>
      </template>
      <template #default="{ item }">
        <div class="relative w-full h-60" @click="navigateToDetails">
          <NuxtImg
              class="object-cover w-full h-full cursor-pointer"
              :src="item.url"
              alt="Image de l'annonce"
          />
        </div>
      </template>
      <template #prev="{ onClick, disabled }">
        <button
            @click.stop="onClick"
            type="button"
            class="opacity-0 group-hover/card:opacity-100 transition-all flex items-center justify-center rounded-full bg-white text-secondaire hover:bg-white hover:text-secondaire disabled:bg-white absolute top-1/2 left-1 transform -translate-y-1/2 p-1"
            :disabled="disabled"
        >
          <UIcon name="material-symbols:chevron-left" class="w-4 h-4"/>
        </button>
      </template>
      <template #next="{ onClick, disabled }">
        <button
            @click.stop="onClick"
            type="button"
            class="opacity-0 group-hover/card:opacity-100 transition-all flex items-center justify-center rounded-full bg-white text-secondaire hover:bg-white hover:text-secondaire disabled:bg-white absolute top-1/2 right-1 transform -translate-y-1/2 p-1"
            :disabled="disabled"
        >
          <UIcon name="material-symbols:chevron-right" class="w-4 h-4"/>
        </button>
      </template>
    </UCarousel>


    <div @click="navigateToDetails" class="cursor-pointer">
      <UIcon
          name="lets-icons:favorite-duotone"
          class="z-20 absolute top-4 right-4 w-8 h-8 text-white"
      />
      <div class="flex flex-col px-6 py-2 text-secondaire">
        <h2 class="text-base font-albert font-semibold flex items-center gap-2">
          {{ logement.type }} <span class="h-1 w-1 rounded-full bg-secondaire"></span> {{
            logement.adresse.ville ?? ''
          }}
        </h2>
        <p class="text-lg font-albert font-bold">Appartement T3 Quartier Mabilais</p>
        <div class="flex flex-wrap justify-between mt-2">
          <div class="flex items-center gap-1 text-gray-400">
            <p class="font-albert text-base font-medium">
              {{ logement.chambre }}ch
            </p>
            <span class="h-1 w-1 rounded-full bg-gray-400"></span>
            <p class="font-albert text-base font-medium">
              {{ logement.sdo }}sdo
            </p>
            <span class="h-1 w-1 rounded-full bg-gray-400"></span>
            <p class="font-albert text-base font-medium">
              {{ logement.superficie }}m²
            </p>
          </div>
          <p class="font-jakarta text-gray-400 flex items-center gap-1 text-xs">
            <span class="text-principal text-xl font-extrabold">{{ logement.loyer }}€</span>/mois
          </p>
        </div>
      </div>
    </div>
  </div>
</template>