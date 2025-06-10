<script setup lang="ts">
import {useLogementsStore} from "~/stores/logements";
import {logementFixtures} from "~/utils/fixtures/logements";
import type {Logement} from "~/models/entities/logement";
import {dividerUI} from "~/utils/config/ui/divider";

// const route = useRoute()

// const logementStore = useLogementsStore();
// await logementStore.getLogementById(route.params.uuid as string);
// const {logement} = storeToRefs(logementStore);

let logement = ref<Logement | null>(null);

onMounted(() => {
  logement.value = logementFixtures[0];
});
</script>

<template>
  <div class="mx-auto flex flex-col gap-8 md:pt-14 md:pb-56 lg:pt-[87px] lg:pb-0 max-w-[1400px]" v-if="logement">
    <article class="flex flex-col justify-between gap-8 lg:gap-8">
      <LogementImagesGallery :logement="logement" class="md:px-8"/>
      <div class="relative flex flex-nowrap max-xl:flex-col gap-8 px-4 md:px-8">
        <div class="flex-1">
          <div class="xl:max-w-[90%] flex flex-col gap-4 lg:gap-8 mb-8">
            <LogementOverview :logement="logement" class-name="xl:hidden"/>
            <LogementTitle :logement="logement" class="hidden xl:block"/>
            <LogementDescription :logement="logement"/>
            <UDivider :ui="dividerUI"/>
            <LogementCaracteristics/>
            <UDivider :ui="dividerUI"/>
            <div class="flex max-lg:flex-col gap-8">
              <LogementEnergyClass :logement="logement"/>
              <LogementGesClass :logement="logement"/>
            </div>
            <UDivider :ui="dividerUI"/>
            <LogementFinancialConditions :logement="logement"/>
            <UDivider :ui="dividerUI"/>
            <LogementMapPreview :logement="logement"/>
            <UDivider :ui="dividerUI"/>
          </div>
        </div>
          <LogementOverview :logement="logement" class-name="hidden xl:flex flex-col gap-4 sticky top-40"/>
      </div>
    </article>
    <div class="px-4 pb-12">
      <RecentSection :logements="logementFixtures" title="Logements similaires" :ui="{
          container: 'w-full relative flex snap-x snap-mandatory scroll-smooth',
          item: ' snap-start basis-full md:basis-1/2 lg:basis-1/3 xl:basis-1/4',
        }"/>
    </div>
  </div>
</template>
