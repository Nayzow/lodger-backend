<script setup lang="ts">
import { ref, reactive, computed, watch } from 'vue';
import FileUpload from '../../../../components/shared/FileUpload.vue';
import type {UpdateDossierDto} from "~/models/dto/update-dossier-dto";
import {StatutPro} from "~/models/enums/statutPro";
import {Gender} from "~/models/enums/gender";
import {StatusFamilial} from "~/models/enums/statutFamilial";
import {Garant} from "~/models/enums/garant";

const form = reactive<UpdateDossierDto>({
  statutProfessionnel: 'CDI',
  revenuMensuel: '',
  garant: '',
  revenuGarant: '',
  statusFamiliale: 'Marié',
  nbColocataires: '',
  animaux: 'Oui',
  fumeur: 'Oui',
  commentaire: '',
  files: [] // Initialisation du tableau de fichiers
});

const handleFileUploaded = (fileType: string, file: File): void => {
  const existingFileIndex = form.files.findIndex(f => f.type === fileType);
  if (existingFileIndex !== -1) {
    // Remplace le fichier existant
    form.files[existingFileIndex] = { name: file.name, type: fileType, data: file };
  } else {
    // Ajoute un nouveau fichier
    form.files.push({ name: file.name, type: fileType, data: file });
  }
};

// Watch pour log les modifications dans le formulaire
watch(
  () => form,
  (newForm) => {
    console.log('Formulaire mis à jour :', newForm);
  },
  { deep: true }
);

// Computed properties to determine which document fields to show
const showFamilleDocuments = computed(() => form.garant === 'Famille' || form.garant === 'Tiers');
const showVisaleDocuments = computed(() => form.garant === 'Garantie visale');
const showCautioneoDocuments = computed(() => form.garant === 'Garantie Cautioneo');
const showGarantmeDocuments = computed(() => form.garant === 'Garantie Garantme');
const showUnkleDocuments = computed(() => form.garant === 'Garantie Unkle');
const showPersonneMoraleDocuments = computed(() => form.garant === 'Personne morale');

// Tooltips content
const tooltips = {
  extraitKbis: "Le Kbis est la carte d'identité officielle d'une entreprise enregistrée en France, qui prouve son existence légale",
  justificatifSolvabilite: "Exemple de documents : \nBilans comptables récents\nAttestation de fonds\nDerniers comptes annuels\nAttestation d'un expert-comptable si c'est une petite structure",
  preuveEngagement: "Statuts de la société ou document désignant les pouvoirs du signataire"
};

const statutProOptions = Object.values(StatutPro).map(value => ({label: value, value}));
const statutFamilialOption = Object.values(StatusFamilial).map(value => ({label: value, value}));
const garantOptions = Object.values(Garant).map(value => ({label: value, value}));


</script>

<template>
  <div class="lg:bg-white lg:rounded-3xl lg:border lg:border-gray-200 p-8 w-full max-w-4xl shadow-sm">
    <div class="space-y-6">
      <h2 class="text-2xl font-semibold">Informations du dossier</h2>

      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <!-- Statut professionnel -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Statut professionnel</label>
          <div class="relative">
            <select
                v-model="form.statutProfessionnel"
                class="w-full py-3 px-4 border border-gray-200 rounded-xl appearance-none pr-10 focus:outline-none focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500"
            >
              <option value="">Sélectionner un statut</option>
              <option v-for="option in statutProOptions" :key="option.value" :value="option.value">
                {{ option.label }}
              </option>
            </select>
            <div class="absolute inset-y-0 right-0 flex items-center px-3 pointer-events-none">
              <svg class="w-4 h-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"
                   xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7"></path>
              </svg>
            </div>
          </div>
        </div>

        <!-- Revenu mensuel net -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Revenu mensuel net</label>
          <input
              type="text"
              v-model="form.revenuMensuel"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500"
          />
        </div>

        <!-- Garant -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Garant</label>
          <div class="relative">
            <select
                v-model="form.garant"
                class="w-full py-3 px-4 border border-gray-200 rounded-xl appearance-none pr-10 focus:outline-none focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500"
            >
              <option value="">Sélectionner un garant</option>
              <option v-for="option in garantOptions" :key="option.value" :value="option.value">
                {{ option.label }}
              </option>
            </select>
            <div class="absolute inset-y-0 right-0 flex items-center px-3 pointer-events-none">
              <svg class="w-4 h-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"
                   xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7"></path>
              </svg>
            </div>
          </div>
        </div>

        <!-- Revenu Garant -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Revenu Garant</label>
          <input
              type="text"
              v-model="form.revenuGarant"
              class="w-full py-3 px-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500"
          />
        </div>

        <!-- Status Familiale -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Status Familiale</label>
          <div class="relative">
            <select
                v-model="form.statusFamiliale"
                class="w-full py-3 px-4 border border-gray-200 rounded-xl appearance-none pr-10 focus:outline-none focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500"
            >
              <option value="">Sélectionner un statut</option>
              <option v-for="option in statutFamilialOption" :key="option.value" :value="option.value">
                {{ option.label }}
              </option>
            </select>
            <div class="absolute inset-y-0 right-0 flex items-center px-3 pointer-events-none">
              <svg class="w-4 h-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"
                   xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7"></path>
              </svg>
            </div>
          </div>
        </div>

        <!-- Nb de colocataires -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Nb de colocataires</label>
          <div class="relative">
            <select
                v-model="form.nbColocataires"
                class="w-full py-3 px-4 border border-gray-200 rounded-xl appearance-none pr-10 focus:outline-none focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500"
            >
              <option value="">Colocataires</option>
              <option value="0">0</option>
              <option value="1">1</option>
              <option value="2">2</option>
              <option value="3">3</option>
              <option value="4+">4+</option>
            </select>
            <div class="absolute inset-y-0 right-0 flex flex-col justify-center px-3 pointer-events-none">
              <svg class="w-3 h-3 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"
                   xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 15l7-7 7 7"></path>
              </svg>
              <svg class="w-3 h-3 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"
                   xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7"></path>
              </svg>
            </div>
          </div>
        </div>

        <!-- Fumeur -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Fumeur</label>
          <div class="flex space-x-4">
            <label
                class="flex items-center space-x-2 px-5 py-2 rounded-full cursor-pointer"
                :class="form.fumeur === 'Oui' ? 'border border-emerald-400' : 'border border-gray-200'"
            >
              <div class="relative flex items-center justify-center w-5 h-5">
                <input
                    type="radio"
                    v-model="form.fumeur"
                    value="Oui"
                    class="sr-only"
                />
                <div
                    class="w-5 h-5 rounded-full border-2"
                    :class="form.fumeur === 'Oui' ? 'border-emerald-400' : 'border-gray-300'"
                ></div>
                <div
                    v-if="form.fumeur === 'Oui'"
                    class="absolute w-3 h-3 bg-emerald-400 rounded-full"
                ></div>
              </div>
              <span class="text-sm">Oui</span>
            </label>
            <label
                class="flex items-center space-x-2 px-5 py-2 rounded-full cursor-pointer"
                :class="form.fumeur === 'Non' ? 'border border-emerald-400' : 'border border-gray-200'"
            >
              <div class="relative flex items-center justify-center w-5 h-5">
                <input
                    type="radio"
                    v-model="form.fumeur"
                    value="Non"
                    class="sr-only"
                />
                <div
                    class="w-5 h-5 rounded-full border-2"
                    :class="form.fumeur === 'Non' ? 'border-emerald-400' : 'border-gray-300'"
                ></div>
                <div
                    v-if="form.fumeur === 'Non'"
                    class="absolute w-3 h-3 bg-emerald-400 rounded-full"
                ></div>
              </div>
              <span class="text-sm">Non</span>
            </label>
          </div>
        </div>

        <!-- Animaux -->
        <div class="space-y-2 grid-col-span-1">
          <label class="block text-teal-800 font-medium text-sm">Animaux</label>
          <div class="flex space-x-4">
            <label
                class="flex items-center space-x-2 px-5 py-2 rounded-full cursor-pointer"
                :class="form.animaux === 'Oui' ? 'border border-emerald-400' : 'border border-gray-200'"
            >
              <div class="relative flex items-center justify-center w-5 h-5">
                <input
                    type="radio"
                    v-model="form.animaux"
                    value="Oui"
                    class="sr-only"
                />
                <div
                    class="w-5 h-5 rounded-full border-2"
                    :class="form.animaux === 'Oui' ? 'border-emerald-400' : 'border-gray-300'"
                ></div>
                <div
                    v-if="form.animaux === 'Oui'"
                    class="absolute w-3 h-3 bg-emerald-400 rounded-full"
                ></div>
              </div>
              <span class="text-sm">Oui</span>
            </label>
            <label
                class="flex items-center space-x-2 px-5 py-2 rounded-full cursor-pointer"
                :class="form.animaux === 'Non' ? 'border border-emerald-400' : 'border border-gray-200'"
            >
              <div class="relative flex items-center justify-center w-5 h-5">
                <input
                    type="radio"
                    v-model="form.animaux"
                    value="Non"
                    class="sr-only"
                />
                <div
                    class="w-5 h-5 rounded-full border-2"
                    :class="form.animaux === 'Non' ? 'border-emerald-400' : 'border-gray-300'"
                ></div>
                <div
                    v-if="form.animaux === 'Non'"
                    class="absolute w-3 h-3 bg-emerald-400 rounded-full"
                ></div>
              </div>
              <span class="text-sm">Non</span>
            </label>
          </div>
        </div>
      </div>


      <hr class="border-gray-200 my-6"/>

      <h2 class="text-2xl font-semibold">Documents à fournir</h2>

      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <!-- Documents première colonne -->
        <div class="space-y-6">
          <!-- Justificatif de domicile -->
          <div class="space-y-2">
            <FileUpload
                label="Justificatif de domicile"
                @file-uploaded="(file) => handleFileUploaded('justificatif', file)"
            />
          </div>

          <!-- Attestation d'hébergement -->
          <div class="space-y-2">
            <FileUpload
                label="Attestation d'hébergement (si concerné)"
                @file-uploaded="(file) => handleFileUploaded('attestationHebergement', file)"
            />
          </div>

          <!-- Attestation de foyer fiscal -->
          <div class="space-y-2">
            <FileUpload
                label="Attestation de foyer fiscal"
                @file-uploaded="(file) => handleFileUploaded('attestationFoyer', file)"
            />
          </div>
        </div>

        <!-- Documents deuxième colonne -->
        <div class="space-y-6">
          <!-- Avis d'imposition -->
          <div class="space-y-2">
            <FileUpload
                label="Avis d'imposition"
                @file-uploaded="(file) => handleFileUploaded('avisImposition', file)"
            />
          </div>

          <!-- 3 derniers bulletins de salaire -->
          <div class="space-y-2">
            <FileUpload
                label="3 derniers bulletins de salaire"
                @file-uploaded="(file) => handleFileUploaded('bulletins', file)"
            />
          </div>

          <!-- Certificat de scolarité -->
          <div class="space-y-2">
            <FileUpload
                label="Certificat de scolarité (si étudiant)"
                @file-uploaded="(file) => handleFileUploaded('certificat', file)"
            />
          </div>
        </div>
      </div>

      <!-- Documents spécifiques au garant -->
      <div v-if="form.garant" class="mt-6">
        <h2 class="text-2xl font-medium mb-6">Documents spécifiques au garant</h2>

        <!-- Documents pour Famille et Tiers -->
        <div v-if="showFamilleDocuments" class="grid grid-cols-1 md:grid-cols-2 gap-6">
          <!-- Contrat de travail du garant -->
          <div class="space-y-2">
            <FileUpload
                label="Contrat de travail du garant"
                @file-uploaded="(file) => handleFileUploaded('contratGarant', file)"
            />
          </div>

          <!-- 3 derniers bulletins de salaire du garant -->
          <div class="space-y-2">
            <FileUpload
                label="3 derniers bulletins de salaire du garant"
                @file-uploaded="(file) => handleFileUploaded('bulletinsGarant', file)"
            />
          </div>

          <!-- Pièce d'identité du garant -->
          <div class="space-y-2">
            <FileUpload
                label="Pièce d'identité du garant"
                @file-uploaded="(file) => handleFileUploaded('pieceIdentiteGarant', file)"
            />
          </div>

          <!-- Justificatif de domicile du garant -->
          <div class="space-y-2">
            <FileUpload
                label="Justificatif de domicile du garant"
                @file-uploaded="(file) => handleFileUploaded('justificatifDomicileGarant', file)"
            />
          </div>
        </div>

        <!-- Documents pour Garantie visale -->
        <div v-if="showVisaleDocuments" class="space-y-6">
          <div class="space-y-2">
            <FileUpload
                label="Attestation de garantie visale"
                @file-uploaded="(file) => handleFileUploaded('attestationGarantie', file)"
            />
          </div>
        </div>

        <!-- Documents pour Garantie Cautioneo -->
        <div v-if="showCautioneoDocuments" class="space-y-6">
          <div class="space-y-2">
            <FileUpload
                label="Attestation de garantie cautioneo"
                @file-uploaded="(file) => handleFileUploaded('attestationGarantie', file)"
            />
          </div>
        </div>

        <!-- Documents pour Garantie Garantme -->
        <div v-if="showGarantmeDocuments" class="space-y-6">
          <div class="space-y-2">
            <FileUpload
                label="Attestation de garantie garantme"
                @file-uploaded="(file) => handleFileUploaded('attestationGarantie', file)"
            />
          </div>
        </div>

        <!-- Documents pour Garantie Unkle -->
        <div v-if="showUnkleDocuments" class="space-y-6">
          <div class="space-y-2">
            <FileUpload
                label="Attestation de garantie unkle"
                @file-uploaded="(file) => handleFileUploaded('attestationGarantie', file)"
            />
          </div>
        </div>

        <!-- Documents pour Personne morale -->
        <div v-if="showPersonneMoraleDocuments" class="grid grid-cols-1 md:grid-cols-2 gap-6">
          <!-- Extrait kbis -->
          <div class="space-y-2">
            <FileUpload
                label="Extrait kbis de société"
                @file-uploaded="(file) => handleFileUploaded('extraitKbis', file)"
                :tooltip="tooltips.extraitKbis"
            />
          </div>

          <!-- Justificatif de solvabilité -->
          <div class="space-y-2">
            <FileUpload
                label="Justificatif de solvabilité de l'entreprise"
                @file-uploaded="(file) => handleFileUploaded('justificatifSolvabilite', file)"
                :tooltip="tooltips.justificatifSolvabilite"
            />
          </div>

          <!-- Pièce d'identité du représentant légal -->
          <div class="space-y-2">
            <FileUpload
                label="Pièce d'identité du représentant légal de la société"
                @file-uploaded="(file) => handleFileUploaded('pieceIdentiteRepresentant', file)"
            />
          </div>

          <!-- Preuve que le représentant légal peut engager la société -->
          <div class="space-y-2">
            <FileUpload
                label="Preuve que le représentant légal peut engager la société"
                @file-uploaded="(file) => handleFileUploaded('preuveEngagement', file)"
                :tooltip="tooltips.preuveEngagement"
            />
          </div>
        </div>
      </div>

      <hr class="border-gray-200 my-6"/>

      <!-- Commentaire -->
      <div class="space-y-2">
        <label class="block text-2xl font-medium">Quelque chose à ajouter ?</label>
        <div class="relative">
          <textarea
              v-model="form.commentaire"
              class="w-full p-6 border border-gray-200 rounded-3xl min-h-[150px] focus:outline-none focus:ring-1 focus:ring-emerald-500 focus:border-emerald-500"
              placeholder="Lorem ipsum dolor sit amet consectetur ibendum nibh sitconsequat pharetra odio."
          ></textarea>
          <div class="absolute bottom-4 right-4 text-gray-400">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"
                 xmlns="http://www.w3.org/2000/svg">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z"></path>
            </svg>
          </div>
        </div>
      </div>

      <div class="flex justify-center">
        <button
            type="submit"
            class="bg-secondaire text-white font-medium py-3 px-6 rounded-xl shadow-md hover:bg-emerald-600 focus:outline-none focus:ring-2 focus:ring-emerald-500 focus:ring-opacity-50"
        >
          Envoyer
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>
