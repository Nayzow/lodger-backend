<script setup lang="ts">


let articles = ref<Array<{ title: string, content: string }>>([]);

onMounted(() => {
  const article = {
    title: "Le prix de l’immobilier en bretagne",
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla nec purus feugiat, molestie ipsum et, consequat nibh.",
  }
  articles.value = Array(10).fill(article) as Array<{ title: string, content: string }>;
});
</script>

<template>
  <div class="order-1 lg:order-2 w-full lg:my-16 lg:mt-12 flex flex-col gap-4">
    <SectionTitle
        class-name="lg:hidden"
        title="Derniers articles"
        description="Les derniers articles de notre blog"
    />
    <UCarousel :items="articles"
               :ui="{
          container: 'w-full relative flex snap-x snap-mandatory scroll-smooth',
          item: ' snap-start basis-full md:basis-1/2 xl:basis-1/3 3xl:basis-1/4 4xl:basis-1/5',
        }">
      <template #default="{ item }">
        <ArticleCard :title="item.title" :content="item.content"/>
      </template>
    </UCarousel>
  </div>
</template>

<style scoped lang="scss">

</style>