<script setup lang="ts">

import {capitalizeFirstLetter} from "~/utils/helpers/stringHelper";
import type {Logement} from "~/models/entities/logement";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  }
});

</script>

<template>
  <h1 class="text-2xl lg:text-3xl xl:text-3xl font-albert text-secondaire font-bold">{{ capitalizeFirstLetter(logement.type) }} Quartier
    {{ logement.adresse.quartier }}</h1>
</template>
