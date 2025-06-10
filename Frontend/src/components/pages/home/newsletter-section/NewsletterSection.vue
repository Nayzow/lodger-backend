<script setup lang="ts">
</script>

<template>
  <div class="order-2 lg:order-1 bg-white border border-gray-300 rounded-xl pt-8 lg:pt-12 px-8 lg:px-16 md:mx-4">
    <div class="flex max-sm:flex-col justify-between gap-4 lg:gap-8 2xl:lg:gap-36">
      <div class="order-2 lg:order-1 flex justify-center">
        <NuxtImg
            src="/images/phone-image.png"
            alt="Newsletter"
            class="w-[250px] lg:w-[300px] mx-auto rounded-lg mt-4 lg:mt-0"
            height="300"
        />
      </div>
      <div class="w-full flex-1 order-1 lg:order-2 pb-8">
        <div class="w-full 2xl:max-w-4xl 2xl:mx-auto flex flex-col gap-8">
          <p class="flex-1 flex items-center gap-2 lg:hidden">
            <UIcon name="i-custom-lodger" class="w-8 h-8"/>
            <span class="text-secondaire font-semibold">Lodger</span>
          </p>
          <h2 class="text-secondaire font-semibold">Comment je vais faire pour suivre le marché immobilier ?</h2>
          <p class="text-secondaire font-medium">Conseils, chiffres clés, marché… Notre équipe met tout en oeuvre pour que vous ne
            ratiez aucune opportunité business !</p>
          <NewsletterForm/>
        </div>
      </div>
      <div class="hidden order-3 justify-end lg:flex gap-4">
        <UIcon name="i-custom-lodger" class="w-20 h-20"/>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>