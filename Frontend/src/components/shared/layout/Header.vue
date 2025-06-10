<script setup lang="ts">
import {ref, provide} from 'vue';


const router = useRouter();

const isLogementsPage = computed(() => router.currentRoute.value.path === '/logements');

const isOpen = ref(false);

const toggleSidebar = () => {
  isOpen.value = !isOpen.value;
};

provide('isOpen', isOpen);
provide('toggleSidebar', toggleSidebar);

watchEffect(() => {
  if (!import.meta.client) {
    return;
  }
  document.body.style.overflow = isOpen.value ? 'hidden' : 'auto';
});
</script>

<template>
  <header class="sticky top-0 z-40 bg-white shadow-md">
    <HorizontalNav/>
    <MobileNav/>
    <div class="hidden lg:block" v-if="isLogementsPage">
      <AlertCTA/>
    </div>
  </header>
</template>