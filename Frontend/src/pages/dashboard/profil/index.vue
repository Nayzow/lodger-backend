<script setup lang="ts">

import {useProfileStore} from "~/stores/profile";

const profileStore = useProfileStore();
await profileStore.getProfile();
await profileStore.getAvatar();

</script>

<template>
  <div class="flex flex-col min-h-screen w-full bg-gray-50 py-4">
    <NavBack/>
    <MobileDashboardProfile/>
    <DesktopDashboardProfile/>
    <MobileDashboardBottomNav/>
  </div>
</template>

<style scoped lang="scss">

</style>