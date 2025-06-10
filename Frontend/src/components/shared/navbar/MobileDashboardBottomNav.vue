<script setup lang="ts">

</script>

<template>
  <div class="md:hidden rounded-full shadow-lg bg-white p-2 fixed bottom-2 left-2 right-2 z-10 border border-gray-300">
    <div class="flex justify-between items-center px-4">
      <UButton variant="ghost" color="gray" class="flex flex-col items-center">
        <UIcon name="i-heroicons-magnifying-glass" class="text-xl"/>
        <span class="text-xs mt-1">Explore</span>
      </UButton>

      <UButton variant="ghost" color="gray" class="flex flex-col items-center">
        <UIcon name="i-heroicons-heart" class="text-xl"/>
        <span class="text-xs mt-1">Favoris</span>
      </UButton>

      <UButton variant="ghost" color="gray" class="flex flex-col items-center">
        <UIcon name="i-heroicons-home" class="text-xl"/>
        <span class="text-xs mt-1">Logement</span>
      </UButton>

      <UButton variant="ghost" color="gray" class="flex flex-col items-center">
        <UIcon name="i-heroicons-chat-bubble-left-right" class="text-xl"/>
        <span class="text-xs mt-1">Messages</span>
      </UButton>

      <UButton variant="ghost" class="flex flex-col items-center" to="/dashboard/menu" active-class="text-principal">
        <UIcon name="i-heroicons-bars-3" class="text-xl"/>
        <span class="text-xs mt-1 text-[#02db82]">Menu</span>
      </UButton>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>