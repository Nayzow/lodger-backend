<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";
import {iconButtonUI} from "~/utils/config/ui";

defineProps({
  logement: {
    type: Object as () => Logement,
    required: true
  },
  className: {
    type: String,
    default: ''
  }
});
</script>

<template>
  <div class="flex flex-col h-fit xl:max-w-xl xl:ml-auto gap-4"
       :class="className">
    <div class="flex flex-col gap-4 xl:min-w-[350px] xl:max-w-md xl:bg-white xl:border xl:border-gray-300 xl:rounded-2xl xl:px-8 xl:py-12 xl:shadow-md xl:relative">
      <LogementOverviewTitle :logement="logement"/>
      <LogementTitle :logement="logement" class="xl:hidden"/>
      <LogementOverviewActions :logement="logement"/>
      <UDivider class="hidden xl:block"/>
      <LogementOverviewCTA :logement="logement"/>
      <LogementOverviewFeatures :logement="logement"/>
      <LogementOverviewButton class-name="hidden xl:flex"/>
      <div class="absolute top-2 right-2 flex justify-between items-center gap-1">
        <UButton
            icon="fluent:share-28-regular"
            :ui="iconButtonUI"
            size="sm"
            class="hidden xl:flex bg-white text-secondaire hover:text-white hover:bg-principal w-fit rounded-full shadow-md border"
        />
         <UButton
            icon="ic:round-favorite-border"
            :ui="iconButtonUI"
            size="sm"
            class="hidden xl:flex bg-white text-secondaire hover:text-white hover:bg-principal w-fit rounded-full shadow-md border"
        />

      </div>

    </div>
    <LogementOverviewReport/>
  </div>
</template>
