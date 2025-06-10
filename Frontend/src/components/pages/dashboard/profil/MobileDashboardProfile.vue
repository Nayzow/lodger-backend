<script setup lang="ts">
import {ref} from "vue";
import MobilePersonalInfoSection from "~/components/pages/dashboard/profil/MobilePersonalInfoSection.vue";
import MobileIdentitySection from "~/components/pages/dashboard/profil/MobileIdentitySection.vue";

const activeSection = ref<string | null>(null);
const sections = [
  {
    id: 'personal-info',
    label: 'Mes informations personnelles',
    icon: 'i-heroicons-user',
  },
  {
    id: 'identity-card',
    label: "Carte d'identité",
    icon: 'hugeicons:identity-card',
  },
];

const handleSectionClick = (sectionId: string) => {
  activeSection.value = activeSection.value === sectionId ? null : sectionId;
};
</script>

<template>
  <div class="px-4 md:hidden flex flex-col gap-8 min-h-screen w-full">
    <div class="flex flex-col gap-8" v-if="activeSection === null">
      <h1 class="text-3xl text-black font-albert">Profil</h1>

      <div
          class="border border-gray-300 rounded-3xl shadow-lg bg-white mx-8 py-8 px-4 flex justify-center items-center gap-8"
      >
        <div class="flex-1 flex flex-col items-cente gap-4">
          <DashboardAvatar class-name="h-32 w-32 mx-auto"/>
          <div class="flex flex-col items-center">
            <p class="font-semibold text-black font-albert">Pierre Gasly</p>
            <p class="text-xs text-gray-500 font-albert">29 ans</p>
          </div>
        </div>

        <div class="flex-1 flex flex-col items-center gap-4">
          <div class="flex flex-col w-full">
            <p class="font-semibold text-principal font-albert">86%</p>
            <p class="text-sm font-albert">Eligibilité</p>
            <UDivider class="w-full"/>
          </div>
          <div class="flex flex-col w-full">
            <p class="font-semibold font-albert">833k €</p>
            <p class="text-sm 500 font-albert">Revenu</p>
            <UDivider class="w-full"/>
          </div>
          <div class="flex flex-col w-full">
            <p class="font-semibold font-albert">CDI</p>
            <p class="text-sm font-albert">Statut Pro</p>
            <UDivider class="w-full"/>
          </div>
        </div>
      </div>

      <div>
        <div v-for="section in sections" :key="section.id">
          <button
              @click="handleSectionClick(section.id)"
              class="flex justify-between items-center py-3 px-4 rounded-lg hover:bg-gray-100 transition duration-200 w-full"
          >
          <span class="flex items-center">
            <UIcon :name="section.icon" class="text-secondaire mr-3 w-8 h-8"/>
            <span class="font-albert">{{ section.label }}</span>
          </span>
            <UIcon name="i-heroicons-chevron-right"/>
          </button>
          <UDivider class="my-2"/>
        </div>
      </div>
    </div>

    <div v-else>
      <MobilePersonalInfoSection
          v-if="activeSection === 'personal-info'"
          :resetActiveSection="() => { if (activeSection) activeSection = null; }"
      />
      <MobileIdentitySection
          v-if="activeSection === 'identity-card'"
          :resetActiveSection="() => { if (activeSection) activeSection = null; }"
      />
    </div>
  </div>
</template>