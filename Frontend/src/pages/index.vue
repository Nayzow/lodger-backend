<script setup lang="ts">
import {logementFixtures} from "~/utils/fixtures/logements";
import {ref} from "vue";
import type {Logement} from "~/models/entities/logement";


let logements = ref<Logement[]>([]);
onMounted(() => {
  logements.value = logementFixtures;
});
</script>

<template>
  <div class="flex flex-col gap-8">
    <HeroSection />
    <section class="flex justify-center lg:hidden px-4">
      <SearchForm class="w-full"/>
    </section>
    <div class="md:bg-inherit relative w-full mx-auto flex flex-col gap-14 lg:gap-32 p-4 pt-8 md:p-8 lg:pt-16 xl:px-24 rounded-t-[32px] md:rounded-none shadow-[0_-8px_30px_rgb(0,0,0,0.12)] md:shadow-none">
      <RecentSection :logements="logements" title="Ajoutés récemment" :ui="{
          container: 'w-full relative flex snap-x snap-mandatory scroll-smooth gap-1',
          item: ' snap-start basis-full md:basis-1/2 lg:basis-1/3 2xl:basis-1/4 4xl:basis-1/5 5xl:basis-1/6'
        }"/>
      <CtaSection />
      <FaqSection />
      <div class="flex flex-col gap-8 mb-16">
        <NewsletterSection/>
        <BlogSection/>
      </div>
    </div>
  </div>

</template>
