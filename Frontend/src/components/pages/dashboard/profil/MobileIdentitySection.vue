<script setup lang="ts">
defineProps({
  resetActiveSection: {
    type: Function as PropType<() => void>,
    required: true,
  },
});

const selectedDocType = ref('CNI');
const uploadedFile = ref<File | null>(null);
const uploadedFileName = ref('nomdefoc.pdf');
const isFileUploaded = ref(true); // Pour montrer l'état avec fichier téléchargé comme dans la maquette

const handleFileUpload = (event: Event) => {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files.length > 0) {
    uploadedFile.value = input.files[0];
    uploadedFileName.value = input.files[0].name;
    isFileUploaded.value = true;
  }
};

const handleDragOver = (event: DragEvent) => {
  event.preventDefault();
};

const handleDrop = (event: DragEvent) => {
  event.preventDefault();
  if (event.dataTransfer?.files.length) {
    uploadedFile.value = event.dataTransfer.files[0];
    uploadedFileName.value = event.dataTransfer.files[0].name;
    isFileUploaded.value = true;
  }
};

const submitForm = () => {
  console.log('Document soumis:', {
    type: selectedDocType.value,
    file: uploadedFile.value
  });
};

const removeFile = () => {
  uploadedFile.value = null;
  uploadedFileName.value = '';
  isFileUploaded.value = false;
};
</script>

<template>
  <div class="fixed inset-0 bg-white z-10 overflow-y-auto p-6 pb-20">
    <!-- Bouton retour -->
    <UButton
        icon="i-heroicons-arrow-left"
        size="lg"
        class="xl:hidden bg-white text-secondaire hover:text-white w-fit rounded-full shadow-md border mb-6"
        @click="resetActiveSection"
    />

    <!-- Titre principal -->
    <div class="pb-6">
      <h1 class="text-2xl text-black font-bold font-albert">Documents d'identités</h1>
    </div>

    <div>
      <form @submit.prevent="submitForm">
        <!-- Sélection du type de document -->
        <div class="mb-6">
          <div class="relative border border-gray-300 rounded-2xl p-4 mb-2">
            <div class="flex justify-between items-center">
              <span class="text-gray-700 text-lg">Document d'identité</span>
              <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
              </svg>
            </div>
          </div>
          <p class="text-gray-500 mb-4">Choisissez le document d'identité que vous souhaitez fournir</p>

          <!-- Options de documents avec radio buttons -->
          <div class="border border-gray-300 rounded-3xl overflow-hidden mb-6">
            <div class="p-4">
              <!-- Carte d'identité -->
              <div class="flex justify-between items-center py-2" @click="selectedDocType = 'CNI'">
                <label for="carte" class="text-gray-700 text-lg">Carte d'identité</label>
                <div
                    class="w-6 h-6 rounded-full border-2 flex items-center justify-center"
                    :class="selectedDocType === 'CNI' ? 'border-green-500' : 'border-gray-300'"
                >
                  <div
                      v-if="selectedDocType === 'CNI'"
                      class="w-4 h-4 rounded-full bg-green-500"
                  ></div>
                </div>
              </div>

              <!-- Séparateur -->
              <div class="h-px bg-green-500 my-2"></div>

              <!-- Passeport -->
              <div class="flex justify-between items-center py-2" @click="selectedDocType = 'passeport'">
                <label for="passeport" class="text-gray-700 text-lg">Passeport</label>
                <div
                    class="w-6 h-6 rounded-full border-2 flex items-center justify-center"
                    :class="selectedDocType === 'passeport' ? 'border-green-500' : 'border-gray-300'"
                >
                  <div
                      v-if="selectedDocType === 'passeport'"
                      class="w-4 h-4 rounded-full bg-green-500"
                  ></div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Zone de téléchargement -->
        <div class="mb-6">
          <h3 class="text-secondaire text-lg font-medium mb-4">Passeport ou Carte d'identité (recto et verso)</h3>

          <!-- Zone de drop pour le fichier -->
          <div
              v-if="!isFileUploaded"
              class="border border-dashed border-gray-300 rounded-3xl p-4 text-center cursor-pointer mb-4"
              @dragover="handleDragOver"
              @drop="handleDrop"
              @click="$refs.fileInput.click()"
          >
            <div class="text-secondaire text-lg font-medium ">Télécharger un document ici</div>
            <div class="text-gray-400">Ou cliquer-glisser votre fichier</div>
            <input
                ref="fileInput"
                type="file"
                class="hidden"
                @change="handleFileUpload"
                accept=".pdf,.jpg,.jpeg,.png"
            />
          </div>

          <!-- Affichage du fichier téléchargé -->
          <div
              v-else
              class="border border-green-500 rounded-3xl p-4 text-center mb-4"
          >
            <div class="flex flex-col justify-between items-center">
              <div class="text-gray-800 font-medium">Document téléchargé</div>
              <div class="flex items-center">
                <a href="#" class="text-secondaire underline">{{ uploadedFileName }}</a>
                <button @click="removeFile" class="ml-1 text-gray-500">x</button>
              </div>
            </div>
          </div>
        </div>

        <!-- Bouton Terminer -->
        <div class="flex justify-center mb-20">
          <button
              type="submit"
              class="bg-secondaire text-white rounded-2xl shadow-md px-12 py-3 font-semibold text-lg"
          >
            Terminer
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>