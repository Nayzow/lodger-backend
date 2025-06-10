<script setup lang="ts">
import type {Logement} from "~/models/entities/logement";
import ProductListLayout from "~/components/shared/layout/ProductListLayout.vue";
import ProductList from "~/components/shared/list/ProductList.vue";

defineProps({
  logements: {
    type: Array as PropType<Logement[]>,
    required: true,
  },
  title: {
    type: String,
    required: true,
  },
  ui: {
    type: Object as PropType<Record<string, string>>,
    default: () => ({}),
  },
});
</script>

<template>
  <ProductListLayout :title="title">
    <template #default>
      <ProductList :logements="logements"  :ui="ui"/>
    </template>
  </ProductListLayout>
</template>
