<script setup lang="ts">
import AuthContainer from "~/components/shared/layout/AuthContainer.vue";

const isOpen = inject<boolean>('isModalOpen');
const formType = inject<string>('formType');

</script>

<template>
  <UModal v-model="isOpen" :ui="{rounded: 'rounded-3xl'}">
    <AuthContainer>
      <LoginForm v-if="formType === 'login'"/>
      <RegisterForm v-else/>
    </AuthContainer>
  </UModal>
</template>
