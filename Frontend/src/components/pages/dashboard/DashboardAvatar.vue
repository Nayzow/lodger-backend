<script setup>
defineProps({
  className: {
    type: String,
    default: '',
  },
})

const {avatar} = useProfileStore();

const fileInput = ref(null);

function triggerAvatarEdit() {
  fileInput.value?.click();
}

function onAvatarChange(event) {
  const file = event.target.files[0];
  if (file) {
    // Ici, ajoutez la logique pour uploader/modifier l'avatar
    // Par exemple : useProfileStore().updateAvatar(file)
    // Pour la démo, on peut afficher un aperçu local si besoin
  }
}
</script>

<template>
  <div
      class="relative"
      @click="triggerAvatarEdit"
  >
    <img
        v-if="avatar"
        :src="avatar"
        alt="Guillaume Laroche"
        class="w-full h-full object-cover"
    />
    <img
        v-else
        src="https://ui-avatars.com/api/?name=Guillaume+Laroche&background=0D8ABC&color=fff&size=128"
        alt="Guillaume Laroche"
        class="w-full h-full object-cover"
    />
    <!-- Icône d'édition visible au hover -->
    <div
        class="absolute inset-0 bg-black bg-opacity-40 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity"
    >
      <UIcon
          name="heroicons-outline:pencil"
          class="text-white w-6 h-6"
      />
    </div>
    <!-- Input file caché -->
    <input
        ref="fileInput"
        type="file"
        accept="image/*"
        class="hidden"
        @change="onAvatarChange"
    />
  </div>
</template>

<style scoped lang="scss">

</style>