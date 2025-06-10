<script setup lang="ts">
import {ref, computed, onMounted, onUnmounted} from 'vue';

const min = defineModel('min', {
  type: Number,
  required: true,
});

const max = defineModel('max', {
  type: Number,
  required: true,
});

useRangeValues(min, max);

const histogramData = Array.from({length: 40}, (_, i) => {
  const x = i / 40;
  const peak1 = Math.exp(-Math.pow((x - 0.3) * 8, 2));
  const peak2 = Math.exp(-Math.pow((x - 0.7) * 8, 2));
  return Math.round((peak1 + peak2) * 1500);
});

const minPrice = computed(() => Math.min(...histogramData));
const maxPrice = computed(() => Math.max(...histogramData));

const maxHistogramValue = computed(() => Math.max(...histogramData));

const activeBarStart = computed(() => {
  if (!min.value || !maxPrice.value || !minPrice.value) return 0;
  return Math.floor((min.value - minPrice.value) / (maxPrice.value - minPrice.value) * (histogramData.length - 1));
});

const activeBarEnd = computed(() => {
  if (!max.value || !maxPrice.value || !minPrice.value) return histogramData.length - 1;
  return Math.ceil((max.value - minPrice.value) / (maxPrice.value - minPrice.value) * (histogramData.length - 1));
});

const sliderRef = ref<HTMLDivElement | null>(null);
const isDragging = ref(false);
const activeThumb = ref<'min' | 'max' | null>(null);

const updateValue = (event: MouseEvent | TouchEvent) => {
  if (!minPrice.value || !maxPrice.value || !min.value || !max.value) return;
  if (!isDragging.value || !sliderRef.value) return;

  const rect = sliderRef.value.getBoundingClientRect();
  let clientX: number;

  if (event instanceof MouseEvent) {
    clientX = event.clientX;
  } else {
    clientX = event.touches[0].clientX;
  }

  const percentage = (clientX - rect.left) / rect.width;
  const value = Math.round(minPrice.value + (maxPrice.value - minPrice.value) * percentage);

  if (activeThumb.value === 'min') {
    min.value = Math.min(Math.max(value, minPrice.value), max.value);
  } else if (activeThumb.value === 'max') {
    max.value = Math.max(Math.min(value, maxPrice.value), min.value);
  }
};

const startDrag = (thumb: 'min' | 'max') => {
  isDragging.value = true;
  activeThumb.value = thumb;
  document.addEventListener('mousemove', updateValue);
  document.addEventListener('touchmove', updateValue);
  document.addEventListener('mouseup', stopDrag);
  document.addEventListener('touchend', stopDrag);
};

const stopDrag = () => {
  isDragging.value = false;
  activeThumb.value = null;
  document.removeEventListener('mousemove', updateValue);
  document.removeEventListener('touchmove', updateValue);
  document.removeEventListener('mouseup', stopDrag);
  document.removeEventListener('touchend', stopDrag);
};

onMounted(() => {
  document.addEventListener('mousemove', updateValue);
  document.addEventListener('touchmove', updateValue);
  document.addEventListener('mouseup', stopDrag);
  document.addEventListener('touchend', stopDrag);
});

onUnmounted(() => {
  document.removeEventListener('mousemove', updateValue);
  document.removeEventListener('touchmove', updateValue);
  document.removeEventListener('mouseup', stopDrag);
  document.removeEventListener('touchend', stopDrag);
});

const minThumbPosition = computed(() => {
  if (min.value === null || maxPrice.value === null || minPrice.value === null) return 0;
  return ((min.value - minPrice.value) / (maxPrice.value - minPrice.value)) * 100;
});

const maxThumbPosition = computed(() => {
  if (max.value === null || maxPrice.value === null || minPrice.value === null) return 100;
  return ((max.value - minPrice.value) / (maxPrice.value - minPrice.value)) * 100;
});
</script>

<template>
  <div class="w-full max-w-3xl mx-auto pb-2 px-2">
    <div class="relative pt-8 z-20">
      <div class="flex items-end h-12 space-x-0.5">
        <div
            v-for="(value, index) in histogramData"
            :key="index"
            class="flex-1 transition-all duration-200"
            :class="{
              'bg-principal': index >= activeBarStart && index <= activeBarEnd,
              'bg-[#D9D9D9]': index < activeBarStart || index > activeBarEnd
            }"
            :style="{ height: `${(value / maxHistogramValue) * 100}%` }"
        />
      </div>
      <div ref="sliderRef" class="absolute inset-x-0 bottom-0 h-5">
        <div class="absolute inset-y-0" :style="{
          left: `${minThumbPosition}%`,
          right: `${100 - maxThumbPosition}%`
        }"></div>
        <div
            class="absolute w-5 h-5 -ml-5 mt-2 rounded-full bg-white shadow-md cursor-pointer"
            :style="{ left: `${minThumbPosition}%` }"
            @mousedown="startDrag('min')"
            @touchstart.prevent="startDrag('min')"
        ></div>
        <div
            class="absolute w-5 h-5 -mr-5 mt-2 rounded-full bg-white shadow-md cursor-pointer"
            :style="{ left: `${maxThumbPosition}%` }"
            @mousedown="startDrag('max')"
            @touchstart.prevent="startDrag('max')"
        ></div>
      </div>
    </div>
  </div>
</template>
