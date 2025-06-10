<script setup>
import { useRoute } from 'vue-router';
import {useProfileStore} from "~/stores/profile.js";
import { ref } from 'vue';

const {avatar} = storeToRefs(useProfileStore());

const route = useRoute();

const menuItems = [
  {
    id: 'profil',
    label: 'Profil',
    icon: 'heroicons-outline:squares-2x2',
    url: '/dashboard/profil',
  },
  {
    id: 'dossier',
    label: 'Dossier',
    icon: 'heroicons-outline:folder',
    url: '/dashboard/dossier',
  },
  {
    id: 'documents',
    label: 'Documents',
    icon: 'heroicons-outline:document',
    url: '/dashboard/documents',
  },
  {
    id: 'location',
    label: 'Ma location',
    icon: 'heroicons-outline:home',
    url: '/dashboard/location',
  },
  {
    id: 'paiement',
    label: 'Paiement',
    icon: 'heroicons-outline:credit-card',
    url: '/dashboard/paiements',
  },
  {
    id: 'parametres',
    label: 'Paramètres',
    icon: 'heroicons-outline:cog-6-tooth',
    url: '/dashboard/parametres',
  },
];

// Ajout pour la modification de l'avatar
const fileInput = ref(null);

function triggerAvatarEdit() {
  fileInput.value?.click();
}

function onAvatarChange(event) {
  const file = event.target.files[0];
  if (file) {
    // Ici, ajoutez la logique pour uploader/modifier l'avatar
    // Par exemple : useProfileStore().updateAvatar(file)
    // Pour la démo, on peut afficher un aperçu local si besoin
  }
}
</script>

<template>
  <div class="hidden bg-white rounded-3xl border border-gray-200 md:flex flex-col min-w-[300px]">
    <!-- Section Profil -->
    <div class="flex flex-col items-center p-6">
      <div
        class="w-24 h-24 rounded-full overflow-hidden mb-4 relative group cursor-pointer"
        @click="triggerAvatarEdit"
      >
        <img
            v-if="avatar"
            :src="avatar"
            alt="Guillaume Laroche"
            class="w-full h-full object-cover"
        />
        <img
            v-else
            src="https://ui-avatars.com/api/?name=Guillaume+Laroche&background=0D8ABC&color=fff&size=128"
            alt="Guillaume Laroche"
            class="w-full h-full object-cover"
        />
        <!-- Icône d'édition visible au hover -->
        <div
          class="absolute inset-0 bg-black bg-opacity-40 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity"
        >
          <UIcon
            name="heroicons-outline:pencil"
            class="text-white w-6 h-6"
          />
        </div>
        <!-- Input file caché -->
        <input
          ref="fileInput"
          type="file"
          accept="image/*"
          class="hidden"
          @change="onAvatarChange"
        />
      </div>
      <h2 class="text-secondaire text-xl font-medium">Guillaume Laroche</h2>
      <div class="w-full h-px bg-green-500 mt-4"></div>
    </div>

    <!-- Menu Items -->
    <nav class="flex-1 pb-4">
      <ul>
        <li
            v-for="item in menuItems"
            :key="item.id"
            class="p-3"
            :class="{ 'bg-gray-100 border-l border-principal': route.path === item.url }"
        >
          <ULink
              :to="item.url"
              class="flex items-center text-secondaire hover:text-teal-900 transition-colors w-full"
              :class="{ 'font-medium': route.path === item.url }"
          >
            <UIcon :name="item.icon" class="text-secondaire mr-3 w-8 h-8" />
            <span>{{ item.label }}</span>
          </ULink>
        </li>
      </ul>
    </nav>
  </div>
</template>

<style scoped>
</style>
