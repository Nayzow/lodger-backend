import { ref } from 'vue'

const closeAllModalsEvent = ref(0)

export function useModalCloser() {
  const triggerCloseAllModals = () => {
    closeAllModalsEvent.value++
  }

  return {
    closeAllModalsEvent,
    triggerCloseAllModals
  }
}