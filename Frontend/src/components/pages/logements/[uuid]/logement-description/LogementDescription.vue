<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  }
});

const isExpanded = ref(false);
</script>

<template>
  <div class="flex flex-col gap-8">
    <h3 class="text-md md:text-lg lg:text-xl">Description :</h3>
    <div class="flex flex-col gap-4">
      <p class="font-medium text-black">{{ logement.description }}</p>
    </div>
    <UButton
        :icon="isExpanded ? 'line-md:chevron-down' : 'line-md:chevron-right'"
        size="xl"
        class="w-fit underline text-black font-semibold font-jakarta bg-transparent hover:bg-transparent shadow-none p-0 flex-row-reverse leading-tight"
        @click="isExpanded = !isExpanded"
    >
      En savoir plus
    </UButton>
    <div v-if="isExpanded" class="flex flex-col gap-4">
      <h3 class="text-md md:text-lg lg:text-xl">Equipements :</h3>
      <div class="flex flex-col gap-2 font-medium ">
        <p class="font-medium text-black">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut purus eget nunc. Nullam nec nisl nec
          nunc
          ultricies
          fermentum.
        </p>
        <p class="font-medium text-black">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut purus eget nunc. Nullam nec nisl nec
          nunc
          ultricies
          fermentum.
        </p>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>