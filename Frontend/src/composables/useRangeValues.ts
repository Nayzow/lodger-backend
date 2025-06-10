import {watch, ref} from 'vue';

export function useRangeValues(min: Ref<number | null>, max: Ref<number |null>, maxGap: number = 100) {
  let prevMinValue = ref(min.value);
  let prevMaxValue = ref(max.value);

  watch(
      () => [min.value, max.value],
      ([newMin, newMax]) => {
        if (newMin === null || newMax === null) {
          return;
        }
        if (newMax - newMin < maxGap) {
          if (newMin !== prevMinValue.value || newMax !== prevMaxValue.value) {
            min.value = prevMinValue.value;
            max.value = prevMaxValue.value;
          }
          return;
        }

        prevMinValue.value = newMin;
        prevMaxValue.value = newMax;
      },
      {deep: true, immediate: true}
  );
}