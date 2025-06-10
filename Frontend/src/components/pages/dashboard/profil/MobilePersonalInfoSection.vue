<script setup lang="ts">
import {Gender} from "~/models/enums/gender";
import {Nationality} from "~/models/enums/nationality";
import type {ProfileInfoDto} from "~/models/dto/profile-info-dto";

defineProps({
  resetActiveSection: {
    type: Function as PropType<() => void>,
    required: true,
  },
});


const form = ref<ProfileInfoDto>({
  nom: '',
  prenom: '',
  email: '',
  telephone: '',
  genre: Gender.HOMME,
  nationalite: Nationality.FRANCAIS,
  dateNaissance: '',
  adresse: '',
  codePostal: ''
});

const genreOptions = Object.values(Gender).map(value => ({label: value, value}));

const nationaliteOptions = Object.values(Nationality).map(value => ({label: value, value}));

const submitForm = () => {
  // Logique de soumission du formulaire
  console.log('Formulaire soumis:', form.value);
};

const formGroupUI = {
  label: {
    base: "text-secondaire"
  }
}

const inputUI = {
  rounded: 'rounded-xl',
  padding: {
    md: 'p-3'
  }
}

</script>

<template>
  <div class="fixed inset-0 bg-white z-10 overflow-y-auto p-6 pb-20">
    <UButton
        icon="i-heroicons-arrow-left"
        size="lg"
        class="xl:hidden bg-white text-secondaire hover:text-white w-fit rounded-full shadow-md border mb-6"
        @click="resetActiveSection"
    />
    <h1 class="text-3xl text-black font-albert">Informations personnelles</h1>
    <h2 class="text-xl text-black font-albert mt-4">Données personnelles</h2>
    <div class="mobile-form-container py-4">
      <form @submit.prevent="submitForm">
        <!-- Nom -->
        <div class="mb-4">
          <UFormGroup label="Nom" class="form-group" :ui="formGroupUI">
            <UInput v-model="form.nom" placeholder="Laroche" size="md" :ui="inputUI"/>
          </UFormGroup>
        </div>

        <!-- Prénom -->
        <div class="mb-4">
          <UFormGroup label="Prénom" class="form-group" :ui="formGroupUI">
            <UInput v-model="form.prenom" placeholder="Guillaume" size="md" :ui="inputUI"/>
          </UFormGroup>
        </div>

        <!-- Email -->
        <div class="mb-4">
          <UFormGroup label="Email" class="form-group" :ui="formGroupUI">
            <UInput v-model="form.email" placeholder="guillaumelaroche@gmail.com" type="email" size="md" :ui="inputUI"/>
          </UFormGroup>
        </div>

        <!-- Téléphone -->
        <div class="mb-4">
          <UFormGroup label="Téléphone" class="form-group" :ui="formGroupUI">
            <div class="relative">
              <UInput v-model="form.telephone" placeholder="06 12 45 69 88" type="tel" size="md" :ui="inputUI"/>
              <div class="absolute right-3 top-1/2 transform -translate-y-1/2 flex items-center">
                <img src="https://flagcdn.com/w20/fr.png" alt="France" class="h-4 w-6"/>
              </div>
            </div>
          </UFormGroup>
        </div>

        <!-- Genre -->
        <div class="mb-4">
          <UFormGroup label="Genre" class="form-group" :ui="formGroupUI">
            <USelect
                v-model="form.genre"
                :options="genreOptions"
                placeholder="Homme"

            />
          </UFormGroup>
        </div>

        <!-- Nationalité -->
        <div class="mb-4">
          <UFormGroup label="Nationalité" class="form-group" :ui="formGroupUI">
            <USelect
                v-model="form.nationalite"
                :options="nationaliteOptions"
                placeholder="Français"

            />
          </UFormGroup>
        </div>

        <!-- Date de naissance -->
        <div class="mb-4">
          <UFormGroup label="Date de naissance" class="form-group" :ui="formGroupUI">
            <UInput v-model="form.dateNaissance" placeholder="JJ/MM/AAAA" size="md" :ui="inputUI"/>
          </UFormGroup>
        </div>

        <!-- Adresse actuelle -->
        <div class="mb-4">
          <UFormGroup label="Adresse actuelle" class="form-group" :ui="formGroupUI">
            <UInput v-model="form.adresse" placeholder="13 Allée De Navarre, Rennes, France." size="md" :ui="inputUI"/>
          </UFormGroup>
        </div>

        <!-- Code Postal -->
        <div class="mb-4">
          <UFormGroup label="Code Postal" class="form-group" :ui="formGroupUI">
            <UInput v-model="form.codePostal" placeholder="35000" size="md" :ui="inputUI"/>
          </UFormGroup>
        </div>

        <!-- Bouton Terminer -->
        <div class="mt-6 flex justify-center">
          <button
              type="submit"
              class="bg-secondaire text-white rounded-2xl shadow-md px-6 py-2 font-semibold transition duration-200"
          >
            Terminer
          </button>
        </div>
      </form>
    </div>
  </div>
</template>