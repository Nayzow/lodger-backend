<script setup lang="ts">
const router = useRouter();
const {checkAuth} = useAuthStore();

const isHomepage = computed(() => router.currentRoute.value.path === '/');
const isLogementPage = computed(() => router.currentRoute.value.path === '/logements');

const toggleModal = inject<() => void>('toggleModal');

const actionButtons = [
  {
    icon: "i-material-symbols:search",
    condition: () => true,
    click: () => router.push('/logements'),
    class: "order-1", iconClass: "text-[#686868] w-6 h-6", text: "Explorer"
  },
  {
    icon: "i-custom-heart",
    condition: () => true,
    class: "order-2",
    iconClass: "text-[#686868] w-6 h-6",
    text: "Favoris"
  },
  {
    icon: "mage:user-circle",
    condition: () => !checkAuth(),
    click: toggleModal,
    class: "order-3",
    iconClass: "text-[#686868] w-6 h-6",
    text: "Profil"
  },
  {
    icon: "i-custom-logo-2", condition: () => checkAuth(), click: () => {
    }, class: "order-3", iconClass: "text-[#686868] w-6 h-6", text: "Biens"
  },
  {
    icon: "solar:chat-square-linear", condition: () => checkAuth(), click: () => {
    }, class: "order-4", iconClass: "text-[#686868] w-6 h-6", text: "Messages"
  },
  {
    icon: "mage:user-circle", condition: () => checkAuth(), click: () => {
    }, class: "order-5", iconClass: "text-[#686868] w-6 h-6", text: "Profil"
  },
];

</script>

<template>
  <nav class="fixed bottom-0 w-full xl:hidden z-40 bg-[#FBFBFB] border border-gray-300 flex justify-between items-center p-3" v-if="isHomepage || isLogementPage">
        <ActionButtonList :action-buttons="actionButtons" class-names="w-full max-w-[300px] justify-between mx-auto" button-container-class-name="flex-1"/>
  </nav>
</template>

