<script setup lang="ts">
const router = useRouter();
const isLogementPage = computed(() => router.currentRoute.value.path.includes('/logements]'));

</script>

<template>
  <footer
      class="bg-white mx-auto font-jakarta p-4 lg:px-12 xl:px-24 relative text-gray-600 border flex flex-col gap-8 items-center"
      :class="{'pb-56 md:pb-8 lg:pb-56 xl:pb-16' : isLogementPage}">
    <div class="w-full flex flex-col lg:flex-row justify-between gap-8">
      <div class="flex-1">
        <div class="flex items-center mb-4">
          <NuxtLink to="/" class="flex items-center gap-2 sm:gap-6">
            <UIcon name="i-custom-logo" class="w-[150px] h-[50px] sm:w-[200px] sm:h-[70px]"/>
          </NuxtLink>
        </div>
        <p>Lorem ipsum dolor sit amet consectetur.</p>
        <p>Amet molestie eget tellus pretium orci accumsan</p>
      </div>
      <div class="flex-1 flex">
        <div class="flex-1 flex flex-col gap-4">
          <h3 class="font-semibold text-gray-800">Column Two</h3>
          <nav>
            <ul class="flex flex-col gap-2">
              <li><a href="#">Link Six</a></li>
              <li><a href="#">Link Seven</a></li>
              <li><a href="#">Link Eight</a></li>
              <li><a href="#">Link Nine</a></li>
              <li><a href="#">Link Ten</a></li>
            </ul>
          </nav>
        </div>
        <div class="flex-1 flex flex-col gap-4">
          <h3 class="font-semibold text-gray-800">Column Three</h3>
          <nav>
            <ul class="flex flex-col gap-2">
              <li><a href="#">Link Eleven</a></li>
              <li><a href="#">Link Twelve</a></li>
              <li><a href="#">Link Thirteen</a></li>
              <li><a href="#">Link Fourteen</a></li>
              <li><a href="#">Link Fifteen</a></li>
            </ul>
          </nav>
        </div>
      </div>
      <div class="flex-1 flex flex-col gap-4">
        <h3 class="font-semibold text-gray-800">Subscribe</h3>
        <div class="flex flex-col gap-2 text-xs">
          <p>Inscrivez-vous à notre newsletter pour recevoir des mises à jour et des offres exclusives.</p>
          <form class="flex w-full max-w-md gap-4">
            <input
                type="email"
                placeholder="Entrez votre email"
                class="w-full px-3 py-2.5 text-white border border-gray-400 rounded-xl"
            />
            <UButton
                type="submit"
                class="bg-secondaire hover:bg-principal px-3 py-2.5 text-white text-sm font-semibold rounded-xl"
            >
              S'abonner
            </UButton>
          </form>
          <p>By subscribing you agree to with our <a href="#"
                                                     class="text-principal hover:text-secondaire underline focus:outline-none focus:ring-0">Privacy
            Policy</a> and provide consent to receive updates from our company.</p>
        </div>
      </div>
    </div>
    <p class="font-bold text-3xl">Ce site est une version de démonstration et peut ne pas refléter les fonctionnalités
      et informations de la version finale.</p>
  </footer>
</template>