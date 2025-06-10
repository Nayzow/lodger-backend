<script setup>
import {ref} from 'vue';

const props = defineProps({
  label: {
    type: String,
    default: 'Télécharger un document ici'
  },
  subLabel: {
    type: String,
    default: 'Ou cliquer-glisser votre fichier'
  },
  acceptedFileTypes: {
    type: String,
    default: '.pdf,.jpg,.jpeg,.png'
  },
  tooltip: {
    type: String,
  }
});

const emit = defineEmits(['file-uploaded']);

const uploadedFile = ref(null);
const isFileUploaded = ref(false);
const fileInput = ref(null);

const handleFileUpload = (event) => {
  const input = event.target;
  if (input.files && input.files.length > 0) {
    uploadedFile.value = input.files[0];
    isFileUploaded.value = true;
    emit('file-uploaded', uploadedFile.value);
  }
};

const handleDragOver = (event) => {
  event.preventDefault();
};

const handleDrop = (event) => {
  event.preventDefault();
  if (event.dataTransfer?.files.length) {
    uploadedFile.value = event.dataTransfer.files[0];
    isFileUploaded.value = true;
    emit('file-uploaded', uploadedFile.value);
  }
};
</script>

<template>
  <div>
    <div class="flex items-center justify-between mb-4">
      <label v-if="props.label" class="block text-secondaire mb-2">
        {{ props.label }}
      </label>
      <div class="relative group" v-if="tooltip">
        <svg class="w-4 h-4 text-gray-500 cursor-help" fill="none" stroke="currentColor" viewBox="0 0 24 24"
             xmlns="http://www.w3.org/2000/svg">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
        </svg>
        <div
            class="absolute bottom-full left-1/2 transform -translate-x-1/2 mb-2 w-64 p-2 bg-gray-800 text-white text-xs rounded shadow-lg opacity-0 group-hover:opacity-100 transition-opacity duration-200 pointer-events-none">
          {{ tooltip }}
        </div>
      </div>
    </div>
    <div
        class="rounded-lg p-6 text-center cursor-pointer"
        :class="{'border border-principal' : isFileUploaded,
                 'border border-dashed border-gray-300 ': !isFileUploaded
                }"
        @dragover="handleDragOver"
        @drop="handleDrop"
        @click="fileInput.click()"
    >
      <div class="text-secondaire font-medium">{{ isFileUploaded ? "Document téléchargé" : props.label }}</div>
      <div class="text-gray-400 text-sm flex justify-center items-center gap-2 mt-2"
           :class="{'underline text-secondaire': isFileUploaded}">
        {{ isFileUploaded ? uploadedFile.name : props.subLabel }}
        <!--        button with x icon for delete -->
        <button
            v-if="isFileUploaded"
            @click.stop="() => { isFileUploaded = false; uploadedFile = null; }"
        >
          <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 text-gray-400 hover:text-gray-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
          </svg>
        </button>
      </div>
      <input
          ref="fileInput"
          type="file"
          class="hidden"
          @change="handleFileUpload"
          :accept="props.acceptedFileTypes"
      />
    </div>

  </div>
</template>
