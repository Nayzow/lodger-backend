<script setup lang="ts">
const error = useError();
const handleError = () => {
  clearError({
    redirect: '/',
  });
};
</script>

<template>
  <NuxtLayout>
    <section class="max-w-[1550px] mx-auto flex flex-col items-center justify-center gap-6">
      <template v-if="error?.statusCode === 404">
        <h1 class="text-4xl text-secondaire font-bold">Erreur 404</h1>
        <p>Désole, la page que vous cherchez n'existe pas.</p>
      </template>
      <template v-else>
        <h1 class="text-4xl text-secondaire font-bold">Erreur</h1>
        <p>Une erreur est survenue.</p>
      </template>
      <UButton
          @click="handleError"
          class="bg-principal hover:bg-principal/80 text-white px-4 py-2 rounded-lg"
      >
        Retour à l'accueil
      </UButton>
    </section>
  </NuxtLayout>
</template>