<script setup lang="ts">
import {ref} from 'vue';
import {logementFixtures} from "~/utils/fixtures/logements";
import type {Logement} from "~/models/entities/logement";
import {paginationUI} from "~/utils/config/ui";

const {setQueryParams, serializeQueryParams} = useQueryParamsStore();

const logementStore = useLogementsStore();
await logementStore.getLogements();

const page = ref(1);

async function handleNextPage() {
  setQueryParams({page: page.value});
  await navigateTo({query: serializeQueryParams({page: String(page.value)})});
  await logementStore.searchLogements({page: page.value});
  scrollToTop()
}

function scrollToTop() {
  window.scrollTo({top: 0, behavior: 'smooth'});
}

let logements = ref<Logement[]>([]);
onMounted(() => {
  logements.value = logementFixtures;
});
</script>

<template>
  <LogementSearchTopbar/>
  <div class="relative h-full flex-1 flex max-lg:flex-col">
    <div
        class="h-full relative order-2 lg:order-1 flex-1 flex flex-col pb-8 px-4 xl:pl-10 xl:pr-4 lg:pt-8 rounded-lg gap-4 bg-white">
      <div
          class="block lg:hidden before:absolute before:h-8 before:w-full before:-top-7 before:bg-white before:left-0 before:rounded-t-full before:z-30"/>
      <LogementListHeader :totalElements="logements?.page?.totalElements || 0"/>
      <div class="flex-1 h-full grid grid-cols-1 gap-4 xl:gap-x-6 md:grid-cols-2 2xl:grid-cols-3">
        <LogementCard v-for="logement in logements.slice(0, 2)" :key="logement.id" :logement="logement"
                      class-name="w-full"/>
        <div class="block sm:hidden">
          <AlertCTA class="rounded-lg"/>
        </div>
        <LogementCard v-for="logement in logements.slice(2, 3)" :key="logement.id" :logement="logement"
                      class-name="w-full"/>
        <LogementCard v-for="logement in logements.slice(3, 18)" :key="logement.id" :logement="logement"
                      class-name="w-full"/>
      </div>
      <UPagination
          v-model="page"
          :page-count="6"
          class="mt-8"
          :total="50"
          size="xl"
          @click="handleNextPage()"
          :ui="paginationUI"
      />
    </div>
    <div class="flex-1 flex order-1 lg:order-2 w-full lg:max-w-[30%] xl:max-w-[35%] 4xl:max-w-[45%] 5xl:max-w-[50%]">
      <ClientOnly>
        <Map allow-wheel-zoom :logements="logements"/>
      </ClientOnly>
    </div>
  </div>
</template>
