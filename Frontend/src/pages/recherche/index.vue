<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { storeToRefs } from 'pinia';
import {
  advancedSearchCheckBoxUI,
  advancedSearchFormGroupUI,
} from "~/utils/config/ui";
import { advancedSearchSchema } from "~/models/schemas/search/advancedSearch";
import {
  caracteristiqueOptions,
  chauffageOptions,
  nombreDeChambres,
  nombreDePieces,
  performanceEnergetique,
  typeOptions,
} from "~/utils/config/options/options";
import { removeFromArray } from "~/utils/helpers/formArray";
import { useSearchTermsStore } from "~/stores/searchTerms";
import type { SearchRequest } from "~/models/payloads/searchRequest";

const router = useRouter();
const searchTermsStore = useSearchTermsStore();
const { searchTerms } = storeToRefs(searchTermsStore);

const initialState = (): SearchRequest => ({
  localisation: searchTerms.value.localisation || '',
  type: searchTerms.value.type,
  prixMin: searchTerms.value.prixMin,
  prixMax: searchTerms.value.prixMax,
  surfaceHabitableMin: searchTerms.value.surfaceHabitableMin,
  surfaceHabitableMax: searchTerms.value.surfaceHabitableMax,
  surfaceTerrainMin: searchTerms.value.surfaceTerrainMin,
  surfaceTerrainMax: searchTerms.value.surfaceTerrainMax,
  nombreDePieces: searchTerms.value.nombreDePieces,
  nombreDeChambres: searchTerms.value.nombreDeChambres,
  caracteristiques: searchTerms.value.caracteristiques || [],
  dpo: searchTerms.value.dpo,
  motsCles: searchTerms.value.motsCles || [],
  strict: searchTerms.value.strict,
});

const {
  state,
  onSubmit,
  handleReset,
  addMotCle,
  newMotCle,
} = useSearchForm<SearchRequest>(initialState);

</script>

<template>
  <section class="relative w-full xl:max-w-[1440px] mx-auto flex flex-col gap-8 py-8">
    <div class="px-4 lg:px-24 ">
      <UButton
          icon="i-heroicons-arrow-left"
          size="lg"
          class="xl:hidden bg-white text-secondaire hover:text-white w-fit rounded-full shadow-md border mb-6"
          @click="router.back()"
      />
      <h1 class="font-ninetea text-xl md:text-2xl lg:text-3xl font-bold">Recherche avancée</h1>
    </div>
    <UForm
        :schema="advancedSearchSchema"
        :state="state"
        @submit="onSubmit"
        class="flex flex-col gap-8"
    >
      <div class="w-full flex flex-col gap-12 px-4 lg:px-24">
        <AdvancedSearchField
            label="Où recherchez vous ?"
            name="localisation"
            v-model="state.localisation"
            placeholder="Ville, code postal, département..."
            icon="material-symbols:location-on"
        >
          <PreviousSearch @select="state.localisation = $event" :is-search-page="true"/>
        </AdvancedSearchField>

          <AdvancedSearchField
            label="Budget ?"
            name="budget"
            type="range"
            v-model:modelValueMin="state.prixMin"
            v-model:modelValueMax="state.prixMax"
            icon="i-custom-euro"
        />

        <AdvancedSearchField
            label="Quel type de biens ?"
            name="type"
            type="button-group"
            v-model="state.type"
            :options="typeOptions"
        />

        <div class="flex 2xl:max-w-[60%] max-lg:flex-col gap-4 xl:justify-between">
          <AdvancedSearchField
              label="Surface habitable ?"
              name="surfaceHabitable"
              type="range"
              v-model:modelValueMin="state.surfaceHabitableMin"
              v-model:modelValueMax="state.surfaceHabitableMax"
              icon="i-custom-surface"
          />

          <AdvancedSearchField
              label="Surface de terrain ?"
              name="surfaceTerrain"
              type="range"
              v-model:modelValueMin="state.surfaceTerrainMin"
              v-model:modelValueMax="state.surfaceTerrainMax"
              icon="i-custom-surface"
          />
        </div>

        <AdvancedSearchField
            label="Combien de pièces ?"
            name="nombreDePieces"
            type="button-group"
            v-model="state.nombreDePieces"
            :options="nombreDePieces"
        />

        <AdvancedSearchField
            label="Caractéristiques ?"
            name="caracteristiques"
            type="button-group"
            v-model="state.caracteristiques"
            :options="caracteristiqueOptions"
            :multiple="true"
        />

        <AdvancedSearchField
            label="Combien de chambres ?"
            name="nombreDeChambres"
            type="button-group"
            v-model="state.nombreDeChambres"
            :options="nombreDeChambres"
        />

        <AdvancedSearchField
            label="Chauffage"
            name="chauffage"
            type="button-group"
            v-model="state.chauffage"
            :options="chauffageOptions"
        />

        <!-- DPO section -->
        <UFormGroup
            :ui="advancedSearchFormGroupUI"
            name="dpo"
            class="relative"
        >
          <template #label>
            <div class="w-full 2xl:max-w-xl flex items-center gap-4 lg:gap-8">
              <label>Classe énergie</label>
              <div class="border border-gray-400 rounded-full px-8 py-2.5 bg-white shadow-md text-xs" v-if="state.dpo">
                {{ performanceEnergetique.find((dpo) => dpo.value === state.dpo)?.conso }}
              </div>
            </div>
          </template>
          <div class="flex gap-2 md:gap-4 lg:gap-6 mt-4">
            <UButton
                v-for="dpo in performanceEnergetique"
                :key="dpo.value"
                @click="state.dpo = dpo.value"
                size="xl"
                :style="`background-color: ${dpo.color}; scale: ${state.dpo === dpo.value ? 1.3 : 1}; margin: 0 ${state.dpo === dpo.value ? '0.5rem' : '0'};`"
                class="text-white border shadow flex items-center justify-center px-5 rounded-xl font-semibol transition-all w-10 h-8 md:w-12 md:h-10 lg:w-16 lg:h-12"
            >
              {{ dpo.label }}
            </UButton>
          </div>
        </UFormGroup>

        <AdvancedSearchField
            label="Recherche par mots clés"
            name="motsCles"
            v-model="newMotCle"
            :disabled="state.motsCles?.length && state.motsCles.length >= 4"
            placeholder="Mots clés..."
            @keydown.enter.prevent="addMotCle"
        >
          <div class="flex flex-wrap gap-2 mt-2">
            <UButton
                v-for="motCle in state.motsCles"
                :key="motCle"
                @click="removeFromArray(motCle, state.motsCles)"
                class="bg-principal text-white text-xs rounded-full"
            >
              {{ motCle }} <span class="ml-2">x</span>
            </UButton>
          </div>
        </AdvancedSearchField>

        <UFormGroup
            :ui="advancedSearchFormGroupUI"
            name="strict"
        >
          <div class="p-4 rounded-2xl border shadow xl:max-w-[70%]">
            <UCheckbox
                v-model="state.strict"
                :ui="advancedSearchCheckBoxUI"
                label="Voir uniquement les biens correspondant à tous mes critères"
            />
          </div>
        </UFormGroup>
      </div>
      <div
          class="w-full flex items-center justify-center gap-6 fixed lg:static bottom-0 border lg:border-0 bg-white lg:bg-transparent p-4 z-10"
      >
        <button
            type="button"
            @click="handleReset"
            class="text-secondaire hover:underline"
        >
          Tout effacer
        </button>

        <UButton
            size="xl"
            type="submit"
            class="bg-principal lg:bg-secondaire text-white hover:bg-principal transition-colors px-8 py-3.5 rounded-2xl"
        >
          Rechercher
        </UButton>
      </div>
    </UForm>
  </section>
</template>