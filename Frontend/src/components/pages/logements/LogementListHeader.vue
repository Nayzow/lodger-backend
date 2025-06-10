<script setup lang="ts">

const {totalElements} = defineProps({
  totalElements: {
    type: Number,
    required: true,
  }
});

const searchTermsStore = useSearchTermsStore();
const {searchTerms} = storeToRefs(searchTermsStore);

</script>

<template>
  <div class="flex max-lg:flex-col justify-between items-center">
    <h1 class="text-center">
      <span class="font-bold">{{ totalElements || 0 }} biens</span>
      {{ searchTerms.localisation ? ` à louer à ${searchTerms.localisation}` : '' }}
    </h1>
    <LogementActionButtons/>
  </div>
</template>

<style scoped lang="scss">

</style>