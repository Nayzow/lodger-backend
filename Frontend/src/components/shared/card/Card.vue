<script setup lang="ts">

const props = defineProps({
  class: String,
})
</script>

<template>
  <div class="bg-secondaire text-white rounded-lg p-4"
       :class="props.class">
    <slot></slot>
  </div>
</template>
