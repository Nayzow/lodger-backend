<script setup lang="ts">
import {basicSearchSchema} from "~/models/schemas/search/basicSearch";
import type {SearchRequest} from "~/models/payloads/searchRequest";
import {computed} from "vue";

defineProps({
  className: {
    type: String,
    default: '',
  },
})
const route = useRoute();

const isHomePage = computed(() => route.matched[0].path === '/');
const isLogementSearchPage = computed(() => route.matched[0].path === '/logements');

const searchTermsStore = useSearchTermsStore();
const {searchTerms} = storeToRefs(searchTermsStore);

const initialState = (): SearchRequest => ({
  type: searchTerms.value?.type,
  localisation: searchTerms.value.localisation,
  prixMin: searchTerms.value.prixMin,
  prixMax: searchTerms.value.prixMax,
});

const {state, onSubmit, handleReset} = useSearchForm<SearchRequest>(initialState);

// watch(() => state, (state) => console.log(state), {deep: true});
</script>

<template>
  <UForm :schema="basicSearchSchema" :state="state"
         :class="className" id="search-form">
    <DesktopSearchBar :state="state" :submit="onSubmit"/>
    <MobileSearchBar v-if="isHomePage" :state="state" :submit="onSubmit"/>
    <MobileModalSearchForm v-if="isLogementSearchPage" :state="state" :reset="handleReset" :submit="onSubmit"/>
  </UForm>
</template>
