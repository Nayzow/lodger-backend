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
  <div class="flex flex-col font-jakarta gap-8">
    <h3 class="text-md lg:text-xl 2xl:text-2xl font-bold text-secondaire">La localisation</h3>
    <ClientOnly>
      <Map class-name="h-[350px] lg:h-[500px] 2xl:h-[600px] 3xl:h-[700px]"
           :style="{borderRadius: '1rem'}"
           :center="logement.position"
           :allowWheelZoom="true"/>
    </ClientOnly>
  </div>
</template>

<style scoped lang="scss">

</style>