<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";

defineProps({
  title: {
    type: String,
    required: true
  },
  classes: {
    type: Array as () => Array<{ name: string, color: string }>,
    required: true
  },
  activeClass: {
    type: String,
    required: true
  }
});
</script>

<template>
  <div class="flex flex-col gap-8">
    <h3 class="text-md md:text-lg lg:text-xl text-black flex items-center gap-4">
      {{ title }}
      <UPopover mode="hover">
        <UIcon name="i-custom-info-circle"/>
        <template #panel>
          <slot name="popover"/>
        </template>
      </UPopover>
    </h3>
    <div class="flex gap-2">
      <div v-for="energyClass in classes"
           :key="energyClass.name"
           class="flex items-center justify-center h-6 w-9 rounded-md text-white text-xs font-bold"
           :class="{'scale-150 mx-2': activeClass === energyClass.name}"
           :style="{backgroundColor: energyClass.color}">
        {{ energyClass.name }}
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">

</style>