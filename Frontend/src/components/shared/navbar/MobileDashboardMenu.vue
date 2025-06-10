<template>
  <div class="relative dashboard-nav bg-gray-50 min-h-screen w-full flex flex-col px-3.5 py-6">
    <!-- Header -->
    <div class="pb-0">
      <h1 class="text-2xl font-bold mb-6 text-black font-albert">Menu</h1>

      <!-- User Profile -->
      <ULink class="flex items-center justify-between mb-2" to="/dashboard/profil">
        <div class="flex items-center">
          <DashboardAvatar class-name="h-16 w-16"/>
          <div class="ml-3">
            <h2 class="font-semibold text-black font-albert">Louis Dubois</h2>
            <p class="text-xs text-gray-500 font-albert">Afficher le profil</p>
          </div>
        </div>
        <UButton variant="ghost" color="gray" icon="i-heroicons-chevron-right"/>
      </ULink>

      <!-- Green Line Separator -->
      <div class="h-0.5 bg-[#02db82] w-full my-4"></div>
    </div>

    <!-- Property Listing CTA -->
    <div class="mb-6">
      <UCard
          :ui="{ rounded: 'rounded-3xl', ring: 'ring-1 ring-gray-300', shadow: 'shadow-lg', body: { padding: 'px-6 py-2.5'} }">
        <div class="flex items-center">
          <img
              src="/images/house-dashboard-nav.png"
              alt="Maison"
              class="w-16 h-16 mr-3"
          />
          <div>
            <p class="font-semibold text-secondaire font-albert">Mettre un bien sur <br/> <span
                class="text-principal font-semibold font-albert">Lodger</span>
            </p>

          </div>
        </div>
      </UCard>
    </div>

    <!-- Menu Items -->
    <div class="mb-6">
      <h3 class="text-lg font-medium  font-albert">Paramètres</h3>

      <div class="space-y-0">
        <div v-for="(item, index) in menuItems" :key="index">
          <MobileDashboardMenuItem :item="item"/>
        </div>

      </div>
    </div>

    <!-- Logout -->
    <div class="px-6 mt-auto mb-6 text-center">
      <UButton variant="link" class="text-black">Se déconnecter</UButton>
    </div>

    <!-- Switch to Owner Mode -->
    <div class="px-6 mb-6 text-center">
      <UButton
          color="black"
          class="rounded-full py-3 px-6"
      >
        <UIcon name="i-heroicons-arrows-right-left" class="mr-2"/>
        Passer en mode propriétaire
      </UButton>
    </div>
  </div>
</template>

<script setup>
import DashboardAvatar from "~/components/pages/dashboard/DashboardAvatar.vue";
import MobileDashboardMenuItem from "~/components/shared/navbar/MobileDashboardMenuItem.vue";

const menuItems = [
  {label: 'Dossier', url: '/dashboard/dossier', icon: 'i-heroicons-folder'},
  {label: 'Documents', url: '/dashboard/documents', icon: 'i-heroicons-document'},
  {label: 'Ma location', url: '/dashboard/location', icon: 'i-heroicons-home'},
  {label: 'Notifications', url: '/dashboard/notifications', icon: 'i-heroicons-bell'},
  {label: 'Paiements et versements', url: '/dashboard/paiements', icon: 'i-heroicons-credit-card'},
  {label: 'Parrainage', url: '/dashboard/parrainage', icon: 'i-heroicons-user-group'},
  {label: 'Paramètres', url: '/dashboard/paramètres', icon: 'i-heroicons-cog-6-tooth'},
  {label: "Centre d'aide", url: '/dashboard/help', icon: 'i-heroicons-question-mark-circle'},
]
</script>

<style scoped>
</style>