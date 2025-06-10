<script setup lang="ts">
import {ref, watch, onMounted} from 'vue'
import debounce from 'lodash/debounce'

const emits = defineEmits(['update:modelValue']);

const model = defineModel<string>({
  type: String,
});

const isOpen = defineModel('isOpen', {
  type: Boolean,
  default: false,
});

const props = defineProps({
  submit: Function as PropType<() => void>,
  reset: Function as PropType<() => void>,
});

const {closeAllModalsEvent} = useModalCloser();

watch(closeAllModalsEvent, () => {
  isOpen.value = false;
});

const suggestions = ref<string[]>([]);

const fetchSuggestions = async (query: string) => {
  if (query.length < 3) {
    suggestions.value = [];
    return;
  }

  try {
    const response = await fetch(`https://api-adresse.data.gouv.fr/search/?q=${encodeURIComponent(query)}&limit=5`);
    const data = await response.json();
    suggestions.value = data.features.map((feature: any) => {
      const {city, postcode} = feature.properties;
      return `${city} (${postcode})`;
    });
  } catch (error) {
    console.error('Erreur lors de la récupération des suggestions:', error);
    suggestions.value = [];
  }
};

const debouncedFetchSuggestions = debounce(fetchSuggestions, 300);

watch(model, (newValue) => {
  debouncedFetchSuggestions(newValue as string);
});

const handleSubmit = () => {
  if (!props.submit) return;
  props.submit();
  isOpen.value = false;
}

const updateModel = (value: string) => {
  emits('update:modelValue', value);
};

const clearInput = () => {
  model.value = '';
  suggestions.value = [];
}
</script>

<template>
  <div class="min-h-screen flex flex-col justify-between gap-4 p-4">
    <div class="relative w-full border-2 border-black rounded-3xl px-4 py-2 flex items-center justify-between gap-4">
      <UIcon @click="isOpen = false" name="i-heroicons-arrow-left" class="h-6 w-6"/>
      <UInput v-model="model" placeholder="Où ?" variant="none" class="flex-1" size="xl"
              @update:model-value="updateModel"/>
      <UIcon @click="clearInput()" name="i-heroicons-x-mark" class="h-6 w-6"/>
    </div>
    <div class="flex-1 flex flex-col justify-between">
      <div class="mt-2 flex-1">
        <div v-for="suggestion in suggestions" :key="suggestion"
             @click="model = suggestion;"
             class="p-2 hover:bg-gray-100 cursor-pointer">
          {{ suggestion }}
        </div>
      </div>
      <PreviousSearch :onSelect="updateModel"/>
    </div>
    <div class="flex gap-4">
      <UButton variant="ghost" class="flex-1 justify-center text-gray-700 underline hover:bg-white" @click="reset">
        Tout effacer
      </UButton>
      <UButton icon="material-symbols:search" size="lg" label="Rechercher" @click="handleSubmit()"
               class="flex-1 bg-secondaire justify-center text-white rounded-xl"/>
    </div>
  </div>
</template>

<style scoped lang="scss">
// Vos styles ici
</style>