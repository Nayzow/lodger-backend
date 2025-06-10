<script setup lang="ts">
</script>

<template>
  <section class="flex justify-center max-lg:flex-col gap-4 lg:gap-12 md:mx-4">
    <SectionTitle
        class-name="order-1 lg:hidden"
        title="Questions Fréquentes"
        description="Tout ce que vous devez savoir sur Lodger"
    />
    <div class="flex-1 order-2 lg:order-1">
      <div class="w-full h-[260px] lg:h-[600px]">
        <NuxtImg
            src="/images/faq.png"
            alt="FAQ"
            class="w-full h-full object-cover rounded-xl"
        />
      </div>
    </div>
    <div class="lg:flex-1 flex flex-col gap-8 order-3 lg:order-2 xl:mr-auto">
      <SectionTitle
          class-name="hidden lg:flex"
          title="Questions Fréquentes"
          description="Lorem ipsum dolor sit amet consectetur. Amet molestie eget"
      />
      <FaqAccordion/>
    </div>
  </section>
</template>

<style scoped lang="scss">
</style>