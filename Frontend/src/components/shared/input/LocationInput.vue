<script setup lang="ts">

import {inputUI} from "~/utils/config/ui";
import type {InputSize} from "#ui/types";

const router = useRouter();

const model = defineModel({
  type: String,
});

const {inputStyle} = defineProps({
  inputStyle: {
    type: Object,
  },
  size: {
    type: String as PropType<InputSize>,
    default: 'md',
  },
  placeholder: {
    type: String,
    default: 'Où ?',
  },
  showIcon: {
    type: Boolean,
    default: true,
  },
});

</script>

<template>
  <div class="relative">
    <UInput v-model="model" icon="material-symbols:location-on" :ui="inputStyle ?? inputUI"
            class="relative" :placeholder="placeholder" :size="size"/>
    <UIcon name="material-symbols:close" class="w-5 h-5 text-principal absolute top-1/2 right-2 transform -translate-y-1/2 cursor-pointer"
           @click="model = ''" v-if="showIcon"/>
  </div>
</template>
