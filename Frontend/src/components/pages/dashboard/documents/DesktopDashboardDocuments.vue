<script setup lang="ts">

</script>

<template>
  <div class="lg:bg-white lg:rounded-3xl lg:border lg:border-gray-200 p-8 w-full max-w-4xl shadow-sm">
    <div class="flex items-center gap-3 mb-8">
      <div class="text-gray-800">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
        </svg>
      </div>
      <h1 class="text-2xl font-bold text-gray-800">Documents</h1>
    </div>

    <!-- Search Bar -->
    <div class="relative mb-10">
      <div class="absolute inset-y-0 left-4 flex items-center pointer-events-none">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 text-teal-700" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
        </svg>
      </div>
      <input
          type="text"
          placeholder="Rechercher un document"
          class="w-full py-4 pl-12 pr-4 bg-white border border-gray-200 rounded-full focus:outline-none focus:ring-1 focus:ring-teal-500 focus:border-teal-500 text-gray-500"
      />
    </div>

    <!-- Recent Documents -->
    <div class="mb-10">
      <h2 class="text-xl font-semibold text-teal-800 mb-6">Documents récents</h2>
      <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
        <div v-for="i in 3" :key="i" class="border border-gray-200 rounded-2xl p-6 flex flex-col items-center justify-center hover:border-teal-500 transition-colors cursor-pointer">
          <div class="bg-blue-100 p-4 rounded-xl mb-4">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-blue-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
            </svg>
          </div>
          <p class="text-gray-800 font-medium">Nom du document</p>
        </div>
      </div>
    </div>

    <!-- Document List -->
    <div>
      <h2 class="text-xl font-semibold text-teal-800 mb-6">Liste des documents</h2>

      <!-- Filter Buttons -->
      <div class="flex flex-wrap gap-4 mb-6">
        <button class="flex items-center justify-between px-6 py-4 border border-gray-200 rounded-full text-gray-500 hover:border-teal-500 transition-colors">
          <span>Trier par bien</span>
          <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 ml-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
        </button>
        <button class="flex items-center justify-between px-6 py-4 border border-gray-200 rounded-full text-gray-500 hover:border-teal-500 transition-colors">
          <span>Trier par catégorie</span>
          <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 ml-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
        </button>
        <button class="flex items-center justify-between px-6 py-4 border border-gray-200 rounded-full text-gray-500 hover:border-teal-500 transition-colors">
          <span>Trier par date</span>
          <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 ml-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
        </button>
      </div>

      <!-- Table -->
      <div class="border border-gray-200 rounded-2xl overflow-hidden">
        <!-- Table Header -->
        <div class="grid grid-cols-4 bg-white py-4 px-6 border-b border-gray-200">
          <div class="text-gray-800 font-semibold">Name</div>
          <div class="text-gray-800 font-semibold">Catégories</div>
          <div class="text-gray-800 font-semibold">Partagé par</div>
          <div class="text-gray-800 font-semibold">Date</div>
        </div>

        <!-- Table Rows -->
        <div v-for="i in 2" :key="i" class="grid grid-cols-4 bg-white py-4 px-6 border-b border-gray-100 hover:bg-gray-50 transition-colors">
          <div class="flex items-center gap-3">
            <div class="bg-blue-100 p-2 rounded-lg">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 text-blue-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
              </svg>
            </div>
            <span class="text-gray-800">Nom du document</span>
          </div>
          <div class="flex items-center text-gray-700">Paiements & Loyer</div>
          <div class="flex items-center text-gray-700">M.Laroche</div>
          <div class="flex items-center text-gray-700">11/05/2025</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>