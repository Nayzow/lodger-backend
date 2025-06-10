import {ref} from 'vue'

const expandedField = ref<string | null>(null)

export function useExpandedField() {
  const setExpandedField = (fieldName: string | null) => {
    expandedField.value = fieldName
  }

  const isFieldExpanded = (fieldName: string) => {
    return expandedField.value === fieldName
  }

  return {
    setExpandedField,
    isFieldExpanded
  }
}
