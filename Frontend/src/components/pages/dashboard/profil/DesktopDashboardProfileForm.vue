<script setup lang="ts">
import {ref} from 'vue';
import FileUpload from '../../../../components/shared/FileUpload.vue';
import {Gender} from "~/models/enums/gender.js";
import {Nationality} from "~/models/enums/nationality.js";
import type {ProfileInfoDto} from "~/models/dto/profile-info-dto.js";
import {useProfileStore} from "~/stores/profile";

const {profile} = storeToRefs(useProfileStore());

const form = ref<ProfileInfoDto>({
  nom: profile.value?.nom || '',
  prenom: profile.value?.prenom || '',
  email: profile.value?.email || '',
  telephone: profile.value?.telephone || '',
  genre: profile.value?.genre || Gender.HOMME,
  nationalite: profile.value?.nationalite || Nationality.FRANCAIS,
  dateNaissance: profile.value?.dateNaissance || '',
  adresse: profile.value?.adresse || '',
  codePostal: profile.value?.codePostal || '',
});

const genreOptions = Object.values(Gender).map(value => ({label: value, value}));

const nationaliteOptions = Object.values(Nationality).map(value => ({label: value, value}));

// const uploadedFile = ref<File | null>(null);
//
// const handleFileUploaded = (file: File) => {
//   uploadedFile.value = file;
// };

const submitForm = async () => {
  await useProfileStore().updateProfile(form.value);
};
</script>

<template>
  <div class="bg-white rounded-3xl border border-gray-200 p-8 w-full max-w-4xl shadow-sm">
    <h1 class="text-2xl font-semibold mb-8">Informations personnelles</h1>

    <form @submit.prevent="submitForm">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-x-8 gap-y-6">
        <!-- Nom -->
        <div>
          <label for="nom" class="block text-secondaire mb-2">Nom</label>
          <input
              id="nom"
              v-model="form.nom"
              type="text"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl  focus:outline-none focus:ring-1 focus:ring-teal-500"
          />
        </div>

        <!-- Prénom -->
        <div>
          <label for="prenom" class="block text-secondaire mb-2">Prénom</label>
          <input
              id="prenom"
              v-model="form.prenom"
              type="text"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl  focus:outline-none focus:ring-1 focus:ring-teal-500"
          />
        </div>

        <!-- Email -->
        <div>
          <label for="email" class="block text-secondaire mb-2">Email</label>
          <input
              id="email"
              v-model="form.email"
              type="email"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl  focus:outline-none focus:ring-1 focus:ring-teal-500"
          />
        </div>

        <!-- Téléphone -->
        <div>
          <label for="telephone" class="block text-secondaire mb-2">Téléphone</label>
          <div class="relative">
            <input
                id="telephone"
                v-model="form.telephone"
                type="tel"
                class="w-full py-3 px-4 border border-gray-200 rounded-xl  focus:outline-none focus:ring-1 focus:ring-teal-500 pr-12"
            />
            <div class="absolute right-3 top-1/2 transform -translate-y-1/2 flex items-center">
              <img src="https://flagcdn.com/w20/fr.png" alt="France" class="h-4 w-6" />
            </div>
          </div>
        </div>

        <!-- Genre -->
        <div>
          <label for="genre" class="block text-secondaire mb-2">Genre</label>
          <div class="relative">
            <select
                id="genre"
                v-model="form.genre"
                class="w-full py-3 px-4 border border-gray-200 rounded-xl  appearance-none focus:outline-none focus:ring-1 focus:ring-teal-500 pr-10"
            >
              <option v-for="option in genreOptions" :key="option.value" :value="option.value">
                {{ option.label }}
              </option>
            </select>
            <div class="absolute inset-y-0 right-0 flex items-center px-2 pointer-events-none">
              <svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"></path>
              </svg>
            </div>
          </div>
        </div>

        <!-- Nationalité -->
        <div>
          <label for="nationalite" class="block text-secondaire mb-2">Nationalité</label>
          <div class="relative">
            <select
                id="nationalite"
                v-model="form.nationalite"
                class="w-full py-3 px-4 border border-gray-200 rounded-xl  appearance-none focus:outline-none focus:ring-1 focus:ring-teal-500 pr-10"
            >
              <option v-for="option in nationaliteOptions" :key="option.value" :value="option.value">
                {{ option.label }}
              </option>
            </select>
            <div class="absolute inset-y-0 right-0 flex items-center px-2 pointer-events-none">
              <svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"></path>
              </svg>
            </div>
          </div>
        </div>

        <!-- Date de naissance -->
        <div>
          <label for="dateNaissance" class="block text-secondaire mb-2">Date de naissance</label>
          <input
              id="dateNaissance"
              v-model="form.dateNaissance"
              type="text"
              placeholder="JJ/MM/AAAA"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl  focus:outline-none focus:ring-1 focus:ring-teal-500"
          />
        </div>

        <!-- Code Postal -->
        <div>
          <label for="codePostal" class="block text-secondaire mb-2">Code Postal</label>
          <input
              id="codePostal"
              v-model="form.codePostal"
              type="text"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl  focus:outline-none focus:ring-1 focus:ring-teal-500"
          />
        </div>

        <!-- Adresse actuelle - span 2 columns -->
        <div class="md:col-span-2">
          <label for="adresse" class="block text-secondaire mb-2">Adresse actuelle</label>
          <input
              id="adresse"
              v-model="form.adresse"
              type="text"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl  focus:outline-none focus:ring-1 focus:ring-teal-500"
          />
        </div>

        <!-- Document upload - span 2 columns -->
        <div class="md:col-span-2 mt-2">
          <FileUpload
            label="Passeport ou Carte d'identité (recto et verso)"
            @file-uploaded="handleFileUploaded"
          />
        </div>
      </div>
    </form>
  </div>
</template>
