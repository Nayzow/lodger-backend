<script setup lang="ts">
import {selectButtonUI} from "~/utils/config/ui";
import {ref, watch, computed} from 'vue';

const router = useRouter();
const device = useDevice();
const isOpen = ref(false);
const {closeAllModalsEvent} = useModalCloser();

const searchTermsStore = useSearchTermsStore();
const {searchTerms} = storeToRefs(searchTermsStore);

watch(closeAllModalsEvent, () => {
  isOpen.value = false;
});

const localisationValue = computed(() => {
  if (searchTerms.value.localisation) {
    return searchTerms.value?.localisation.length > 20 ? `${searchTerms.value?.localisation?.substring(0, 20)}...` : searchTerms.value.localisation;
  }
  return '';
});

const otherValues = computed(() => {
  const {type, prixMin, prixMax} = searchTerms.value;
  return device.isMobile ? `${type} / ${prixMin}-${prixMax} €`.substring(0, 20) + ' ...' : `${type} / ${prixMin}-${prixMax} €`;
});

const handleBackClick = () => {
  router.back();
};
</script>

<template>
  <div
      class="sticky top-0 x:relative bg-slate-50 flex justify-between items-center gap-8 p-4 lg:hidden border border-t-1 shadow-2xl z-40">

    <UIcon name="material-symbols:arrow-back"
           class="p-2 text-secondaire w-6 h-6"
           @click="handleBackClick"
    />

    <UButton icon="material-symbols:search"
             size="md"
             variant="outline"
             class="flex-1 px-2 py-1.5 items-center justify-start min-h-[52px] rounded-3xl"
             color="white"
             :ui="selectButtonUI"
             @click="isOpen = true"
    >
      <span v-if="localisationValue" class="font-bold flex flex-col items-start justify-start">
        <span>
          {{ localisationValue }}
        </span>
        <span v-if="otherValues" class="text-xs font-medium">
          {{ otherValues }}
        </span>
      </span>
      <span v-else>Rechercher</span>
    </UButton>

    <UIcon name="i-custom-filter"
           class="p-2 text-secondaire w-6 h-6"
           @click="router.push('/recherche')"
    />
  </div>
  <UModal v-model="isOpen" fullscreen class="z-[999999]">
    <div class="bg-white p-4 flex flex-col gap-4 min-h-screen">
      <UButton icon="material-symbols:close" size="lg" type="button" @click="isOpen = false"
               class="bg-white w-fit justify-center text-secondaire rounded-full border border-gray-300 shadow-md"/>
      <SearchForm class="flex-1"/>
    </div>
  </UModal>
</template>
