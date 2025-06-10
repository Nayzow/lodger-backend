<script setup lang="ts">
import type {PropertyType} from "~/models/types/propertyType";
import {typeOptions} from "~/utils/config/options/options";
import InputIcon from "~/components/shared/input/InputIcon.vue";

const model = defineModel({
  type: String,
  required: true,
});

const value = computed<string | undefined>(() => model.value || 'Quoi ?');

</script>

<template>
  <InputPopoverWrapper label="Type de bien" :value="value">
    <template #default>
      <div class="flex flex-wrap gap-4 max-w-xs text-secondaire">
        <template v-for="option in typeOptions" :key="option.value">
          <InputIcon
              :icon="option.icon"
              :value="option.value"
              :label="option.label"
              :model="model"
              @click="model = option.value as PropertyType"
          />
        </template>
      </div>
    </template>
  </InputPopoverWrapper>
</template>
