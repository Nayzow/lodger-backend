<script setup lang="ts">
import {iconButtonUI} from "~/utils/config/ui";
import SocialButtonList from "~/components/pages/auth/SocialButtonList.vue";

const toggleModal = inject<() => void>('toggleModal', () => {
});
</script>

<template>
  <div class="relative flex flex-col border rounded-3xl">
    <div class="h-14 p-4">
      <UButton
          icon="material-symbols:close-rounded"
          size="md"
          class="text-secondaire hover:text-principal bg-transparent hover:bg-transparent shadow-none "
          :ui="iconButtonUI"
          @click="toggleModal"
      />
    </div>
    <div class="flex flex-col justify-center w-full h-full gap-4 p-8">
      <h1 class="text-4xl text-secondaire font-bold">Bienvenue sur Lodger</h1>
      <div class="flex flex-col gap-4">
        <slot/>
<!--        <div class="flex items-center gap-4">-->
<!--          <hr class="flex-1 h-px bg-gray-300"/>-->
<!--          <span class="text-gray-500">ou</span>-->
<!--          <hr class="flex-1 h-px bg-gray-300"/>-->
<!--        </div>-->
<!--        <SocialButtonList/>-->
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>