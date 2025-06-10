<!-- Composant Vue -->
<script setup lang="ts">
import { ref, computed } from 'vue'
import type { Logement } from "~/models/entities/logement"

const router = useRouter();

const props = defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  }
})

const mainImage = ref(props.logement.photos[0].url)
const isGalleryOpen = ref(false)

const remainingPhotosCount = computed(() => {
  return Math.max(0, props.logement.photos.length - 5)
})

const openGallery = () => {
  isGalleryOpen.value = true
}

const closeGallery = () => {
  isGalleryOpen.value = false
}

const getImageSize = (index: number) => {
  const sizes = [
    'col-span-2 row-span-2',  // Grande image (2x2)
    'col-span-1 row-span-1',  // Petite image (1x1)
    'col-span-1 row-span-1',  // Petite image (1x1)
    'col-span-1 row-span-1',  // Petite image (1x1)
    'col-span-1 row-span-1',  // Petite image (1x1)
    'col-span-2 row-span-2',  // Grande image (2x2)
  ]
  return sizes[index % sizes.length]
}

</script>

<template>
  <div class="w-full" v-if="logement">
    <!-- DESKTOP -->
    <div class="hidden md:flex gap-4 h-[35vh] lg:h-[50vh]">
      <div class="w-1/2 h-full relative">
        <NuxtImg
            :src="mainImage"
            alt="Image principale de l'annonce"
            class="absolute inset-0 w-full h-full object-cover cursor-pointer rounded-tl-2xl rounded-bl-2xl"
            @click="openGallery"
        />
      </div>
      <div class="w-1/2 h-full grid grid-cols-2 gap-4">
        <div v-for="(photo, index) in logement.photos.slice(1, 5)" :key="photo.id" class="relative">
          <NuxtImg
              :src="photo.url"
              :alt="`Image ${index + 1} de l'annonce`"
              class="absolute inset-0 w-full h-full object-cover cursor-pointer"
              @click="openGallery"
              :class="{ 'rounded-tr-2xl': index === 1, 'rounded-br-2xl': index === 3 }"
          />
          <!-- Overlay pour le nombre d'images restantes sur la dernière image -->
          <div
              v-if="index === 3 && remainingPhotosCount > 0"
              class="absolute inset-0 bg-black/50 flex items-center justify-center cursor-pointer"
              :class="{ 'rounded-br-2xl': index === 3 }"
              @click="openGallery"
          >
            <span class="text-white text-5xl font-medium font-ninetea">+{{ remainingPhotosCount }}</span>
          </div>
        </div>
      </div>
    </div>

    <!-- MOBILE -->
    <div class="md:hidden relative">
      <UCarousel
          :ui="{
          wrapper: 'w-full',
          container: 'relative w-full h-[35vh] flex overflow-x-auto snap-x snap-mandatory scroll-smooth',
          item: 'w-full h-full flex-shrink-0 snap-center',
          indicators: {
            wrapper: 'absolute bottom-2 left-1/2 transform -translate-x-1/2 flex gap-0 space-x-0.5',
          }
        }"
          :items="logement.photos"
          indicators
      >
        <template #indicator="{ onClick, page, active }">
          <button
              v-if="page <= 5"
              @click="onClick"
              :class="['w-2 h-2 rounded-full p-1', active || page > 5 ? 'bg-primary' : ' bg-gray-400']"
          />
          <div v-else class="hidden"/>
        </template>
        <template #default="{ item }">
          <NuxtImg
              class="w-full h-full object-cover"
              :src="item.url"
              :alt="'Image de l\'annonce'"
              @click="openGallery"
          />
        </template>

      </UCarousel>

      <div class="p-4 absolute top-2 right-0 flex justify-between items-center gap-4">
        <UButton
            icon="fluent:share-28-regular"
            size="lg"
            class="xl:hidden bg-white text-secondaire hover:text-white hover:bg-principal w-fit rounded-full shadow-md border"
        />
        <UIcon
            name="lets-icons:favorite-duotone"
            class="w-10 h-10 text-white"
        />
      </div>
      <div class="p-4 absolute top-2 left-0 flex justify-between items-center">
        <UButton
            icon="i-heroicons-arrow-left"
            size="lg"
            class="xl:hidden bg-white text-secondaire hover:text-white w-fit rounded-full shadow-md border"
            @click="router.back()"
        />
      </div>
    </div>

    <!-- Modal Gallery -->
    <UModal v-model="isGalleryOpen" fullscreen>
      <div class="flex flex-col h-full bg-white">
        <div class="flex justify-between items-center px-6 py-4 border-b">
          <h2 class="text-xl font-medium">Photos</h2>
          <UButton
              icon="material-symbols:close-rounded"
              size="md"
              class="text-secondaire hover:text-principal bg-transparent hover:bg-transparent shadow-none"
              @click="closeGallery"
          />
        </div>
        <div class="overflow-y-auto px-6 py-4">
          <div class="max-w-7xl mx-auto grid grid-cols-2 md:grid-cols-4 gap-4">
            <div
                v-for="(photo, index) in logement.photos"
                :key="photo.id"
                :class="['relative group aspect-square', getImageSize(index)]"
            >
              <NuxtImg
                  :src="photo.url"
                  :alt="`Image ${index + 1} de l'annonce`"
                  class="w-full h-full object-cover cursor-pointer rounded-lg transition-transform duration-300 hover:scale-[1.02]"
              />
            </div>
          </div>
        </div>
      </div>
    </UModal>
  </div>
</template>

<style scoped>
/* Styles pour assurer une transition fluide sur le hover des images */
.group {
  transform: translateZ(0);
  backface-visibility: hidden;
}

/* Assure que les images ne cassent pas la mise en page lors du chargement */
img {
  display: block;
  width: 100%;
  height: 100%;
}

/* Optimisation du défilement */
.overflow-y-auto {
  -webkit-overflow-scrolling: touch;
  scroll-behavior: smooth;
}
</style>