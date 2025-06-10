<script setup lang="ts">

const navLinks = [
  {to: "/logements", text: "Biens à louer"},
  {to: "/#", text: "Louez votre bien"},
  {to: "/#", text: "Blog"},
  {to: "/#", text: "Nous contacter"}
];

</script>

<template>
    <div class="hidden lg:flex gap-8 font-bold text-lg">
      <ULink
          v-for="link in navLinks"
          :key="link.to"
          :to="link.to"
          active-class="text-principal"
          inactive-class="text-secondaire hover:text-principal"
      >
        {{ link.text }}
      </ULink>
    </div>
</template>

<style scoped lang="scss">

</style>