<script setup lang="ts">
import {iconButtonUI} from "~/utils/config/ui";

defineProps({
  actionButtons: {
    type: Array as PropType<{icon: string, condition: () => boolean, class?: string, iconClass?: string, click?: () => void, text?: string}[]>,
    required: true,
  },
  classNames: {
    type: String,
    default: '',
  },
  buttonContainerClassName: {
    type: String,
    default: '',
  },
});


</script>

<template>
  <div class="flex xl:justify-end items-center md:gap-4" :class="classNames">
    <ULink to="/logements" class="text-lg max-xl:hidden text-secondaire hover:text-principal font-bold" v-if="!$device.isMobile">
      Mettre mon logement sur Lodger
    </ULink>
    <div class="flex justify-between gap-8 xl:gap-4" :class="buttonContainerClassName">
      <template v-for="button in actionButtons" :key="button.icon">
        <UButton
            v-if="button.condition()"
            class="text-secondaire hover:text-principal bg-transparent hover:bg-transparent shadow-none  transition-colors p-0 flex-col gap-1"
            :class="button.class"
            @click="button.click && button.click()"
            :ui="iconButtonUI"
        >
          <UIcon :name="button.icon" :class="button.iconClass"/>
          <span class="block xl:hidden text-[#686868]" v-if="button.text">{{ button.text }}</span>
        </UButton>
      </template>
    </div>
  </div>
</template>
