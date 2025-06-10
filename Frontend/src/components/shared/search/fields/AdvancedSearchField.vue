<script setup lang="ts">
import { computed } from 'vue';
import { advancedSearchFormGroupUI, advancedSearchInputUI } from "~/utils/config/ui";
import { type Option } from "~/models/types/option";

type ModelValue = string | number | boolean | string[];

const props = defineProps({
  label: String,
  name: String,
  type: {
    type: String as () => 'text' | 'range' | 'button-group',
    default: 'text'
  },
  modelValue: [String, Number, Boolean, Array] as PropType<ModelValue>,
  modelValueMin: [Number, String],
  modelValueMax: [Number, String],
  options: {
    type: Array as () => Option[],
    default: () => []
  },
  min: Number,
  max: Number,
  placeholder: String,
  icon: String,
  multiple: Boolean
});

const isMobile = useDevice().isMobile;

const emit = defineEmits(['update:modelValue', 'update:modelValueMin', 'update:modelValueMax']);

const isRangeInput = computed(() => props.type === 'range');
const isButtonGroup = computed(() => props.type === 'button-group');

const updateValue = (value: ModelValue) => {
  emit('update:modelValue', value);
};

const updateRangeValue = (key: 'min' | 'max', value: number | string) => {
  if (key === 'min') {
    emit('update:modelValueMin', value);
  } else {
    emit('update:modelValueMax', value);
  }
};

const isSelected = (option: Option): boolean => {
  if (props.multiple && Array.isArray(props.modelValue)) {
    return props.modelValue.includes(option.value.toString());
  }
  return props.modelValue === option.value;
};

const toggleOption = (option: Option) => {
  console.log("props", props);
  if (props.multiple && Array.isArray(props.modelValue)) {
    const newValue = [...props.modelValue];
    const optionValueString = option.value.toString();
    const index = newValue.indexOf(optionValueString);
    if (index === -1) {
      newValue.push(optionValueString);
    } else {
      newValue.splice(index, 1);
    }
    emit('update:modelValue', newValue);
  } else {
    emit('update:modelValue', option.value);
  }
};

const inputModelValue = computed(() => {
  const value = props.modelValue;
  if (typeof value === 'string' || typeof value === 'number') {
    return value;
  }
  return '';
});

const showAllOptions = ref(false);

const displayedOptions = computed(() => {
  if (!isMobile || showAllOptions.value) {
    return props.options;
  }
  return props.options.slice(0, 9);
});

const showViewMore = computed(() => {
  return isMobile && props.options.length > 10;
});

const viewMoreText = computed(() => {
  return showAllOptions.value ? "Afficher moins" : "Afficher plus";
});

const toggleViewMore = () => {
  showAllOptions.value = !showAllOptions.value;
};
</script>

<template>
  <UFormGroup
      :ui="advancedSearchFormGroupUI"
      :label="label"
      :name="name"
  >
    <template v-if="isRangeInput">
      <div class="flex gap-4">
        <UInput
            :ui="advancedSearchInputUI"
            size="xl"
            :model-value="modelValueMin"
            @update:model-value="updateRangeValue('min', $event)"
            type="number"
            :min="min"
            :icon="icon"
            :placeholder="placeholder || 'Min'"
            class="w-full max-w-[150px]"
        />
        <UInput
            :ui="advancedSearchInputUI"
            size="xl"
            :model-value="modelValueMax"
            @update:model-value="updateRangeValue('max', $event)"
            type="number"
            :min="min"
            :icon="icon"
            :placeholder="placeholder || 'Max'"
            class="w-full max-w-[150px]"
        />
      </div>
    </template>

    <template v-else-if="isButtonGroup">
      <div class="flex flex-col">
        <div class="flex flex-wrap font-jakarta gap-4">
          <UButton
              v-for="option in displayedOptions"
              :key="option.value"
              @click="toggleOption(option)"
              :icon="option.icon"
              size="xl"
              :class="[
          'w-fit bg-white border border-gray-400 shadow-md px-5 rounded-full font-bold text-secondaire font-albert',
          isSelected(option) ? 'bg-principal hover:bg-principal text-white' : 'hover:bg-principal hover:text-white'
        ]"
          >
            {{ option.label }}
          </UButton>
        </div>
        <button
            v-if="showViewMore"
            @click="toggleViewMore"
            class="mt-6 text-left text-principal font-bold underline"
        >
          {{ viewMoreText }}
        </button>
      </div>
    </template>

    <template v-else>
      <UInput
          :ui="advancedSearchInputUI"
          size="xl"
          :model-value="inputModelValue"
          @update:model-value="updateValue"
          :type="type"
          :min="min"
          :icon="icon"
          :placeholder="placeholder"
          class="w-full max-w-[400px]"
      />
      <slot/>
    </template>
  </UFormGroup>
</template>