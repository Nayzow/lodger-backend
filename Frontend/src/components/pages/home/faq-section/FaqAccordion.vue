<script setup lang="ts">
import { ref, onMounted } from 'vue'

interface AccordionItem {
  label: string
  content: string
  defaultOpen?: boolean
}

const openItems = ref<boolean[]>([])

const items: AccordionItem[] = [{
  label: 'Comment créer son dossier de location ? 📁',
  content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed neque elit, tristique placerat feugiat ac, facilisis vitae arcu. Proin eget egestas augue. Praesent ut sem nec arcu pellentesque aliquet. Duis dapibus diam vel metus tempus vulputate.'
}, {
  label: 'Comment s\'organisent les visites ? 🏠',
  content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed neque elit, tristique placerat feugiat ac, facilisis vitae arcu. Proin eget egestas augue. Praesent ut sem nec arcu pellentesque aliquet. Duis dapibus diam vel metus tempus vulputate.'
}, {
  label: 'Comment les locataires sont-ils sélectionnés ? 🔎',
  content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed neque elit, tristique placerat feugiat ac, facilisis vitae arcu. Proin eget egestas augue. Praesent ut sem nec arcu pellentesque aliquet. Duis dapibus diam vel metus tempus vulputate.'
}, {
  label: 'Quelles sont les garanties Lodger ? 🔒',
  content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed neque elit, tristique placerat feugiat ac, facilisis vitae arcu. Proin eget egestas augue. Praesent ut sem nec arcu pellentesque aliquet. Duis dapibus diam vel metus tempus vulputate.'
}]

onMounted(() => {
  const defaultOpenIndex = items.findIndex(item => item.defaultOpen)
  openItems.value = items.map((_, index) => index === defaultOpenIndex)
})

const toggleItem = (index: number) => {
  if (openItems.value[index]) {
    openItems.value[index] = false
  } else {
    openItems.value = openItems.value.map((_, i) => i === index)
  }
}

const onEnter = (el: Element) => {
  (el as HTMLElement).style.height = el.scrollHeight + 'px'
}

const onLeave = (el: Element) => {
  (el as HTMLElement).style.height = '0'
}
</script>

<template>
  <div class="space-y-6">
    <div v-for="(item, index) in items" :key="index" class="rounded-lg bg-white shadow-sm border border-gray-200">
      <button
          @click="toggleItem(index)"
          class="flex w-full items-center justify-between p-4 text-left text-secondaire font-bold font-ninetea hover:bg-gray-50 transition-colors"
          :aria-expanded="openItems[index]"
      >
        <span class="text-lg font-bold">{{ item.label }}</span>
        <UIcon
            :name="openItems[index] ? 'heroicons-solid:chevron-up' : 'heroicons-solid:chevron-down'"
            class="w-10 h-10 text-principal transition-transform duration-300"
            :class="{ 'transform rotate-180': openItems[index] }"
        />
      </button>
      <transition
          name="accordion"
          @enter="onEnter"
          @leave="onLeave"
      >
        <div v-show="openItems[index]" class="overflow-hidden">
          <p class="p-4 pt-0 text-base text-gray-600">
            {{ item.content }}
          </p>
        </div>
      </transition>
    </div>
  </div>
</template>

<style scoped>
.accordion-enter-active,
.accordion-leave-active {
  transition: height 0.5s ease;
}

.accordion-enter-from,
.accordion-leave-to {
  height: 0;
}
</style>