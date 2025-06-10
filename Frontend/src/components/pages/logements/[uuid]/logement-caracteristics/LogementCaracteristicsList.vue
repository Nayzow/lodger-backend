<script setup lang="ts">
import { ref, computed } from 'vue'
import {buttonUI} from "~/utils/config/ui";

const device = useDevice();

const caracteristics = [
  {
    icon: 'tabler:parking',
    label: 'Parking partagé',
    id: 1
  },
  {
    icon: 'lucide:trees',
    label: 'Parc à proximité',
    id: 2
  },
  {
    icon: 'material-symbols:wifi',
    label: 'Wifi',
    id: 3
  },
  {
    icon: 'tabler:bath',
    label: 'Salle de bain privative',
    id: 4
  },
  {
    icon: 'material-symbols:school-outline',
    label: 'Ecoles à proximité',
    id: 5
  },
  {
    icon: 'material-symbols:security',
    label: 'Résidence sécurisée',
    id: 6
  }
]

const showAllCaracteristics = ref(false)

const visibleCaracteristics = computed(() => {
  if (!device.isMobile || showAllCaracteristics.value) {
    return caracteristics
  }
  return caracteristics.slice(0, 4)
})

const remainingCaracteristics = computed(() => {
  if (!device.isMobile|| showAllCaracteristics.value) {
    return 0
  }
  return caracteristics.length - 4
})

const toggleCaracteristics = () => {
  showAllCaracteristics.value = !showAllCaracteristics.value
}

</script>

<template>
  <div>
    <div class="grid grid-cols-2 lg:grid-cols-3 font-jakarta gap-4">
      <LogementCaracteristicItem
          v-for="caracteristic in visibleCaracteristics"
          :key="caracteristic.id"
          :icon="caracteristic.icon"
          :label="caracteristic.label"
      />
    </div>
  </div>
  <div class="flex justify-center lg:hidden pb-4">
    <UButton
        v-if="remainingCaracteristics > 0"
        @click="toggleCaracteristics"
        color="white"
        class="text-md lg:text-xl 2xl:text-2xl px-8"
        size="lg"
        :ui="buttonUI"
    >
      {{ showAllCaracteristics ? 'Voir moins' : `Affficher ${remainingCaracteristics} caractéristiques` }}
    </UButton>
  </div>
</template>

<style scoped lang="scss">
/* Vos styles ici */
</style>

