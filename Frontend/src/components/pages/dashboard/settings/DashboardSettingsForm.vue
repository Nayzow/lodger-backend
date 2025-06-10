<script setup lang="ts">
import {ref} from 'vue';
import type {ChangePasswordDto} from "~/models/dto/change-password-dto";
import type {ConnectedAccountDto} from "~/models/dto/connected-account-dto";
import type {ConnectedDeviceDto} from "~/models/dto/connected-device-dto";

const updatePasswordForm = ref<ChangePasswordDto>({
  currentPassword: '',
  newPassword: '',
  confirmPassword: ''
});

const notificationSettings = ref<UpdateNotificationSettingsDto>({
  push: false,
  email: true,
  sms: true,
  folderActivity: true,
  listingUpdates: true,
  accountSecurity: true
});

const connectedAccounts = ref<ConnectedAccountDto[]>([
  {
    name: 'Google',
    status: 'Connecté'
  }
]);

const connectedDevices = ref<ConnectedDeviceDto[]>([
  {
    type: 'Windows 10',
    location: 'Île de france',
    date: 'Le 24 Octobre 2044 à 11h46'
  },
  {
    type: 'Iphone 44',
    location: 'Île de france',
    date: 'Le 24 Octobre 2044'
  }
]);

const changePassword = () => {
  if (newPassword.value !== confirmPassword.value) {
    alert('Les mots de passe ne correspondent pas');
    return;
  }

  // Logique pour changer le mot de passe
  console.log('Mot de passe changé');
  updatePasswordForm.value.currentPassword = '';
  updatePasswordForm.value.newPassword = '';
  updatePasswordForm.value.confirmPassword = '';
};

const disconnectAccount = (account) => {
  console.log(`Déconnexion du compte ${account.name}`);
};

const disconnectDevice = (device) => {
  console.log(`Déconnexion de l'appareil ${device.type}`);
};

const requestDataExport = () => {
  console.log('Demande d\'export des données');
};

const deleteAccount = () => {
  if (confirm('Êtes-vous sûr de vouloir supprimer votre compte ? Cette action est irréversible.')) {
    console.log('Compte supprimé');
  }
};
</script>

<template>
  <div class="lg:bg-white lg:rounded-3xl lg:border lg:border-gray-200 p-8 w-full max-w-4xl shadow-sm">
    <div class="p-4">
      <h1 class="text-3xl text-black mb-4">Paramètres</h1>
      <div>
        <h2 class="text-xl text-black mb-4">Connexion & Sécurité</h2>
        <p class="text-secondaire mb-3">Authentifications à deux facteurs</p>
        <button class="bg-secondaire text-white px-6 py-2 rounded-xl">
          Activer
        </button>
      </div>
    </div>

    <div class="border-t border-gray-200 my-2"></div>

    <!-- Change password -->
    <div class="p-4">
      <h2 class="font-bold mb-4 text-black">Changer de Mot de passe</h2>

      <div class="mb-4">
        <label class="block text-secondaire mb-2">Mot de passe actuel</label>
        <input
            type="password"
            v-model="updatePasswordForm.currentPassword"
            class="w-full py-3 px-4 border border-gray-200 rounded-xl "
            placeholder="••••••••••"
        />
      </div>

      <div class="mb-4">
        <label class="block text-secondaire mb-2">Nouveau Mot de passe</label>
        <input
            type="password"
            v-model="updatePasswordForm.confirmNewPassword"
            class="w-full py-3 px-4 border border-gray-200 rounded-xl "
            placeholder="••••••••••"
        />
      </div>

      <div class="mb-4">
        <label class="block text-secondaire mb-2">Confirmation du Mot de passe</label>
        <input
            type="password"
            v-model="updatePasswordForm.confirmNewPassword"
            class="w-full py-3 px-4 border border-gray-200 rounded-xl "
            placeholder="••••••••••"
        />
      </div>

      <button
          @click="changePassword"
          class="bg-secondaire text-white px-6 py-2 rounded-xl mt-2"
      >
        Confirmer
      </button>
    </div>

    <div class="border-t border-gray-200 my-2"></div>

    <!-- Linked accounts -->
    <div class="p-4">
      <h2 class="font-bold mb-4 text-black">Comptes lier</h2>

      <div v-for="account in connectedAccounts" :key="account.name" class="flex justify-between items-center mb-2">
        <div>
          <div>{{ account.name }}</div>
          <div class="text-gray-500 text-sm">{{ account.status }}</div>
        </div>
        <button
            @click="disconnectAccount(account)"
            class="text-red-500"
        >
          Déconnection
        </button>
      </div>
    </div>

    <div class="border-t border-gray-200 my-2"></div>

    <!-- Connected devices -->
    <div class="p-4">
      <h2 class="font-bold mb-4 text-black">Appareils connectés</h2>

      <div v-for="(device, index) in connectedDevices" :key="index" class="mb-4">
        <div class="flex justify-between">
          <div>
            <div>{{ device.type }}</div>
            <div class="text-gray-500 text-sm">{{ device.location }} | {{ device.date }}</div>
          </div>
          <button
              @click="disconnectDevice(device)"
              class="text-red-500"
          >
            Déconnection
          </button>
        </div>
      </div>
    </div>

    <div class="border-t border-gray-200 my-2"></div>

    <!-- Notifications -->
    <div class="p-4 flex flex-col gap-4">
      <h2 class="font-bold mb-4 text-black">Notifications</h2>

      <div class="rounded-xl w-full max-w-md">
        <h3 class="text-secondaire mb-3">Préférences de notifications</h3>

        <div class="border border-gray-300 rounded-xl p-4">
          <div class="flex justify-between items-center mb-3">
            <span>Notifications push</span>
            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" v-model="notificationSettings.push" class="sr-only peer">
              <div
                  class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-500"></div>
            </label>
          </div>

          <div class="flex justify-between items-center mb-3">
            <span>E-mail</span>
            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" v-model="notificationSettings.email" class="sr-only peer">
              <div
                  class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-500"></div>
            </label>
          </div>

          <div class="flex justify-between items-center">
            <span>SMS</span>
            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" v-model="notificationSettings.sms" class="sr-only peer">
              <div
                  class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-500"></div>
            </label>
          </div>
        </div>
      </div>

      <div class="border border-gray-300 w-full max-w-md rounded-xl p-4">

        <div class=" rounded-xl mb-4">
          <div class="flex justify-between items-center mb-3">
            <span class="font-medium">Activité du dossier</span>
            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" v-model="notificationSettings.folderActivity" class="sr-only peer">
              <div
                  class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-500"></div>
            </label>
          </div>

          <ul class="text-sm text-gray-600 ml-1">
            <li class="mb-1">Mise à jour des candidatures</li>
            <li class="mb-1">Demande de documents complémentaires</li>
            <li>Invitation à une visite</li>
          </ul>
        </div>
      </div>

      <div class="border border-gray-300 w-full max-w-md rounded-xl p-4">

        <!-- Listing updates notifications -->
        <div class=" rounded-xl mb-4">
          <div class="flex justify-between items-center mb-3">
            <span class="font-medium">Suivi des annonces</span>
            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" v-model="notificationSettings.listingUpdates" class="sr-only peer">
              <div
                  class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-500"></div>
            </label>
          </div>

          <ul class="text-sm text-gray-600 ml-1">
            <li class="mb-1">Mise à jour d'une annonce enregistrée</li>
            <li class="mb-1">Demande de documents complémentaires</li>
            <li>Nouvelle annonce correspondant aux critères</li>
          </ul>
        </div>
      </div>

      <div class="border border-gray-300 w-full max-w-md rounded-xl p-4">
        <!-- Account security notifications -->
        <div class=" rounded-xl mb-4">
          <div class="flex justify-between items-center mb-3">
            <span class="font-medium">Sécurité du compte</span>
            <label class="relative inline-flex items-center cursor-pointer">
              <input type="checkbox" v-model="notificationSettings.accountSecurity" class="sr-only peer">
              <div
                  class="w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-500"></div>
            </label>
          </div>

          <ul class="text-sm text-gray-600 ml-1">
            <li class="mb-1">Changement de mot de passe</li>
            <li>Connexion depuis un nouvel appareil</li>
          </ul>
        </div>
      </div>
    </div>

    <div class="border-t border-gray-200 my-2"></div>

      <!-- Privacy & Data -->
      <div class="p-4">
        <h2 class="text-black font-bold mb-4">Confidentialité & Données</h2>
        <div class="flex max-lg:flex-col justify-between gap-4">
          <div class="mb-4">
            <h3 class="text-lg text-secondaire mb-3">Télécharger mes données (RGPD)</h3>
            <button
                @click="requestDataExport"
                class="bg-secondaire text-white px-6 py-2 rounded-xl"
            >
              Demander l'export
            </button>
          </div>

          <div class="mb-4">
            <h3 class="text-lg text-secondaire mb-3">Supprimer mes données personnelles</h3>
            <button
                @click="deleteAccount"
                class="border border-red-500 text-red-500 px-6 py-2 rounded-xl"
            >
              Supprimer mon compte
            </button>
          </div>
        </div>

      </div>
  </div>
  <!-- Two-factor authentication -->

</template>

<style scoped lang="scss">

</style>