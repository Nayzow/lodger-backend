<script setup lang="ts">

const router = useRouter();

const isHomePage = computed(() => router.currentRoute.value.path === '/');
const {checkAuth} = useAuthStore();

const toggleModal = inject<() => void>('toggleModal');
const toggleSidebar = inject<() => void>('toggleSidebar', () => {

});
const actionButtons = [
  {
    icon: "material-symbols:notifications-outline",
    condition: checkAuth,
    class: "hidden lg:flex",
    iconClass: "w-7 h-7 sm:w-8 sm:h-8 3xl:w-10 3xl:h-10"
  },
  {
    icon: "i-custom-heart",
    condition: () => true,
    class: "order-2 xl:order-1",
    iconClass: "w-7 h-7 sm:w-8 sm:h-8 3xl:w-10 3xl:h-10"
  },
  {
    icon: "i-custom-user",
    condition: () => !checkAuth(),
    click: toggleModal,
    class: "order-1 xl:order-3",
    iconClass: "w-7 h-7 sm:w-8 sm:h-8 3xl:w-10 3xl:h-10"
  },
];

</script>

<template>
  <nav class="after:z-10 font-albert " :class="{'hidden lg:block': !isHomePage}">
    <div
        class="flex justify-between items-center gap-4 w-full py-4 px-4 lg:px-12 xl:px-24 mx-auto bg-[#FBFBFB] border-b border-gray-300">
      <LogoLink/>
      <div class="flex-1 justify-center hidden lg:flex">
        <SearchForm className="w-full max-w-[575px] 2xl:ml-44"/>
      </div>
      <ActionButtonList :action-buttons="actionButtons"/>
    </div>
  </nav>
</template>