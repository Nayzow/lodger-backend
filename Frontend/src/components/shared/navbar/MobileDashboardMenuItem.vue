<script setup lang="ts">
defineProps({
  item: {
    type: Object,
    required: true,
    default: () => ({
      icon: '', // Valeur par défaut pour éviter `undefined`
      url: '',
      label: '',
    }),
  },
});
</script>

<template>
  <ULink
      :href="item.url"
      class="flex justify-between items-center py-3 px-4 rounded-lg hover:bg-gray-100 transition duration-200 w-full"
  >
    <div class="flex items-center">
      <UIcon v-if="item.icon" :name="item.icon" class="text-[#02504d] mr-3 text-xl" />
      <span class="font-albert">{{ item.label }}</span>
    </div>
    <UIcon name="i-heroicons-chevron-right" />
  </ULink>
  <UDivider class="my-2" />
</template>

<style scoped lang="scss">

</style>