<script setup lang="ts">

const article = {
  title: "Le prix de l’immobilier en bretagne",
  content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla nec purus feugiat, molestie ipsum et, consequat nibh.",
}
</script>

<template>
  <div>
    <UCarousel v-if="$device.isMobileOrTablet"
               :items="Array(3).fill(article)"
               :ui="{
                  container: 'w-auto relative flex snap-x snap-mandatory scroll-smooth gap-4 lg:gap-16',
                  item: 'items-center snap-start basis-full md:basis-1/2 lg:basis-1/3',
                }">
      <template #default="{ item }">
        <ArticleCard :title="item.title" :content="item.content"/>
      </template>
    </UCarousel>
    <div v-if="$device.isDesktop" class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-8">
      <ArticleCard v-for="i in 3" :key="i" :title="article.title" :content="article.content"/>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>