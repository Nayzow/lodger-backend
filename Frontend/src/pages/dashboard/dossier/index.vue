<script setup lang="ts">
</script>

<template>
  <div class="flex flex-col min-h-screen w-full bg-gray-50 py-4">
    <NavBack/>
    <div
        class="flex min-h-screen w-full items-start justify-center gap-4 bg-gray-50 py-4 max-w-[1200px] mx-auto">
      <DesktopDashboardMenu/>
      <DesktopDashboardDossierForm/>
    </div>
    <MobileDashboardBottomNav/>
  </div>
</template>

<style scoped lang="scss">

</style>