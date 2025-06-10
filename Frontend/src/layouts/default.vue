<script setup lang="ts">
import {ref, provide} from 'vue';

const isModalOpen = ref(false);
const formType = ref('login');

function toggleModal() {
  isModalOpen.value = !isModalOpen.value;
}

function toggleFormType() {
  formType.value = formType.value === 'login' ? 'register' : 'login';
}

provide('isModalOpen', isModalOpen);
provide('toggleModal', toggleModal);
provide('formType', formType);
provide('toggleFormType', toggleFormType);

</script>
<template>
  <Header/>
  <main class="relative bg-[#fafafa]">
    <slot/>
  </main>
  <Footer/>
  <UNotifications />
  <AuthModal/>
</template>
