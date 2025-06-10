<script setup lang="ts">
defineProps({
  provider: {
    type: String,
    required: true
  },
  icon: {
    type: String,
    required: true
  }
})

const {signIn} = useAuth();

</script>

<template>
  <button @click="signIn(provider.toLowerCase())"
          class="flex items-center gap-2 py-3 px-6 border-2 border-black rounded-xl">
    <Icon :name="icon" class="w-6 h-6"/>
    <span class="flex-1 text-center">
      Continuer avec {{ provider }}
  </span>
  </button>
</template>

<style scoped lang="scss">

</style>