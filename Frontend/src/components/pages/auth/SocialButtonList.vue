<script setup lang="ts">
const socialButtons = [
  {
    provider: 'Google',
    icon: 'i-custom-google'
  },
  {
    provider: 'Facebook',
    icon: 'i-custom-facebook'
  }
]
</script>

<template>
<div class="flex flex-col gap-4">
  <SocialButton v-for="button in socialButtons" :key="button.provider" :provider="button.provider" :icon="button.icon"/>
</div>
</template>

<style scoped lang="scss">

</style>