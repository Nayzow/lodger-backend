<script setup lang="ts">
import {ref} from 'vue'
import {sortOrderOptions} from "~/utils/config/options/options";
import {selectButtonUI} from "~/utils/config/ui";

const router = useRouter();
const {state} = useSortOrder();
const isOpen = ref(false)

const selectOption = (option: { label: string, value: string }) => {
  state.value = option.value
  isOpen.value = false
}
</script>

<template>
  <div class="hidden lg:flex justify-between items-center gap-4">
    <div class="flex items-center font-bold text-secondaire gap-2">
      <p class="font-albert">Trier par :</p>
      <UPopover v-model="isOpen" :ui="{ width: 'w-48' }">
        <span class="flex items-center font-medium gap-1">
          {{ state ? sortOrderOptions.find(o => o.value === state)?.label : 'Sélection' }}
          <UIcon name="i-heroicons-chevron-down" class="ml-1"/>
        </span>

        <template #panel>
          <div class="flex flex-col gap-2 p-2">
            <ul class="flex flex-col gap-2">
              <li v-for="option in sortOrderOptions" :key="option.value" @click="selectOption(option as any)"
                  class="cursor-pointer hover:bg-gray-100 p-2 rounded-lg font-medium"
                  :class="{ 'bg-gray-100': state === option.value }">
                {{ option.label }}
              </li>
              <li class="cursor-pointer hover:bg-gray-100 p-2 rounded-lg font-medium"
                  @click="selectOption({ label: 'Sélection', value: '' })">
                Pas de sélection</li>
            </ul>
          </div>
        </template>
      </UPopover>
    </div>
    <UButton
        :ui="selectButtonUI"
        icon="i-custom-filter"
        label="Filtres"
        size="lg"
        class="rounded-full font-bold font-ninetea px-6 py-2.5"
        color="white"
        @click="router.push('/recherche')"
    />
  </div>
</template>
