<script setup lang="ts">

const prixMin = defineModel<number>('prixMin', {
  type: Number,
  required: true,
});

const prixMax = defineModel<number>('prixMax', {
  type: Number,
  required: true,
});

useRangeValues(prixMin, prixMax);

function formatPrice(prixMin: number, prixMax: number) {
  return `${prixMin} - ${prixMax} €`;
}

</script>

<template>
  <InputPopoverWrapper custom-label="Fourchette de prix" label="Prix" :value="formatPrice(prixMin, prixMax)">
    <template #default>
      <RangeSlider v-model:min="prixMin" v-model:max="prixMax"/>
      <PriceInput
          :prixMin="prixMin"
          :prixMax="prixMax"
          @update:prixMin="prixMin = $event"
          @update:prixMax="prixMax = $event"
      />
    </template>
  </InputPopoverWrapper>
</template>