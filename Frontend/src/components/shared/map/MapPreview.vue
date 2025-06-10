<script setup lang="ts">
import { ref, watch, onMounted, nextTick } from 'vue';

const isOpen = ref(false);

onMounted(async () => {
  const L = await import('leaflet');

  const mapPreview = L.map('mapPreview').setView([47.21322, -1.559482], 13);
  L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors'
  }).addTo(mapPreview);

  watch(isOpen, async (value) => {
    if (value) {
      await nextTick();
      const map = L.map('map').setView([47.21322, -1.559482], 13);
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors'
      }).addTo(map);
    }
  });
});
</script>

<template>
  <div class="w-full bg-white border rounded-lg shadow p-4 mx-auto">
    <div class="flex flex-col gap-4">
      <h3 class="text-lg font-bold">Localisation</h3>
      <p class="text-sm text-secondaire">Rennes, 35000</p>
      <div class="relative w-full h-[250px] rounded-lg overflow-hidden">
        <div id="mapPreview" class="w-full h-full z-20"></div>
      </div>
      <UButton @click="isOpen = true"  class="w-fit bg-principal hover:bg-principal/80 text-white">Voir la carte</UButton>
    </div>

  </div>
  <UModal v-model="isOpen" :ui="{width: 'w-full sm:max-w-3xl'}">
    <div class="p-4">
      <div id="map" class="w-full h-[500px]"></div>
    </div>
  </UModal>
</template>