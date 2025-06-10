<script setup lang="ts">
import { ref } from 'vue';
import { Icon, type PointTuple } from 'leaflet';
import mapMarker from '~/assets/icons/map-marker.svg';
import type {Logement} from "~/models/entities/logement";

const { allowWheelZoom } = defineProps({
  className: {
    type: String,
    required: false,
    default: 'w-full h-[400px] lg:h-screen',
  },
  style: {
    type: Object,
    required: false,
    default: () => ({}),
  },
  center: {
    type: Array as unknown as PropType<PointTuple>,
    required: false,
    default: () => [47.21322, -1.559482] as PointTuple,
  },
  allowWheelZoom: {
    type: Boolean,
    required: false,
    default: false,
  },
  logements: {
    type: Array as PropType<Logement[]>,
    required: false,
    default: () => [],
  }
});

const map = ref<L.Map | null>(null);

const customIcon = new Icon({
  iconUrl: mapMarker,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [-3, -76],
});

const onMapReady = (event: any) => {
  map.value = event;
  map.value?.attributionControl?.remove();

  allowWheelZoom ? map.value?.scrollWheelZoom.enable() :
      map.value?.scrollWheelZoom.disable();
};

const zoomIn = () => {
  map.value?.zoomIn();
};

const zoomOut = () => {
  map.value?.zoomOut();
};
</script>

<template>
  <div :class="className" class="sticky top-[182px]">
    <LMap
        ref="map"
        :zoom="13"
        :center="logements.length ? logements[0].position : center"
        :use-global-leaflet="false"
        :style="style"
        :zoomControl="false"
        @ready="onMapReady"
        class="z-10"
    >
      <LTileLayer
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          layer-type="base"
          name="OpenStreetMap"
      />
      <LMarker v-for="logement in logements" :key="logement.id" :lat-lng="logement.position" :icon="customIcon" />
    </LMap>

    <!-- Custom Zoom Controls -->
    <div class="absolute z-20 right-4 top-4 flex flex-col gap-4">
      <button
          @click="zoomIn"
          class="w-10 h-10 bg-[#004D40] hover:bg-[#00695C] text-white flex items-center justify-center rounded-md transition-colors"
          aria-label="Zoom in"
      >
        <UIcon name="material-symbols:add" class="w-5 h-5"/>
      </button>
      <button
          @click="zoomOut"
          class="w-10 h-10 bg-[#004D40] hover:bg-[#00695C] text-white flex items-center justify-center rounded-md transition-colors"
          aria-label="Zoom out"
      >
        <UIcon name="material-symbols:remove" class="w-5 h-5"/>
      </button>
    </div>
  </div>
</template>

<style scoped>
/* Hide default zoom controls */
:deep(.leaflet-control-zoom) {
  display: none;
}
</style>