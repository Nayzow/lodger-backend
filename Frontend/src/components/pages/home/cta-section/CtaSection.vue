<script setup lang="ts">

const cards = [{
  title: 'Gestion simplifié',
  description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet purus fermentum',
  icon: 'i-custom-hand',
}, {
  title: 'Tarif compétitif',
  description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet purus fermentum',
  icon: 'i-custom-badge',
}, {
  title: 'Données centralisées',
  description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet purus fermentum',
  icon: 'i-custom-data',
}, {
  title: 'Assistance en ligne 7j/7',
  description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet purus fermentum',
  icon: 'i-custom-support',
}]
</script>

<template>
  <section class="flex max-xl:flex-col gap-4 xl:gap-16 md:mx-4">
    <div class="w-full xl:w-1/3 flex flex-col gap-8 justify-center">
      <SectionTitle
          title="Pourquoi choisir Lodger ?"
          description="Lodger est une plateforme de gestion locative qui vous permet de gérer vos biens en toute simplicité."
      />
      <div v-if="$device.isDesktop" class="flex gap-4 md:mx-4">
        <NuxtLink
            to="#"
            class="bg-principal hover:bg-secondaire text-white px-4 py-2.5 rounded-lg text-base transition-colors duration-500"
        >
          Créer un compte
        </NuxtLink>
      </div>
    </div>
    <div class="w-full md:w-fit grid grid-cols-1 md:grid-cols-2 gap-8">
      <template v-for="card in cards" :key="card.title">
        <CtaCard :title="card.title" :description="card.description" :icon="card.icon"/>
      </template>
    </div>
    <div v-if="$device.isMobileOrTablet" class="md:hidden flex justify-center gap-4">
      <NuxtLink
          to="#"
          class="bg-principal hover:bg-principal/80 text-white px-4 py-2 rounded-lg"
      >
        Créer un compte
      </NuxtLink>
    </div>
  </section>
</template>

<style scoped lang="scss">

</style>