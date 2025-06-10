<script setup lang="ts">
import {inputUIPrincipal} from "~/utils/config/ui";

const model = defineModel({
  type: String
});

const value = ref<string>(model.value || 'Où ?');

const oldSearchValues = ref<string[]>([]);

onMounted(() => {
  const searchValues = localStorage.getItem('previousSearches');
  if (searchValues) {
    oldSearchValues.value = JSON.parse(searchValues);
  }
});

watch(model, (newValue) => {
  console.log('new value', newValue);
  value.value = newValue || 'Où ?';
});
</script>

<template>
  <InputPopoverWrapper label="Localisation" :value="value">
    <template #default>
      <div class="flex-1 w-[300px] flex flex-col text-secondaire gap-2">
        <LocationInput v-model="model" :input-style="inputUIPrincipal"/>
        <LastSearchValue v-for="value in oldSearchValues" :key="value" :last-search-value="value"
                         @click="model = value;"/>
      </div>
    </template>
  </InputPopoverWrapper>
</template>