<script setup lang="ts">
import {type NewsletterSchema, newsletterSchema} from "~/models/schemas/newsletter/newsletter";
import type {FormSubmitEvent} from "#ui/types";
import {buttonUI, inputUI} from "~/utils/config/ui";
import {z} from "zod";

const {subscribe} = useNewsletter();
const toast = useToast();
const loading = ref(false);
const state = reactive<NewsletterSchema>({
  email: ''
});

async function onSubmit(event: FormSubmitEvent<NewsletterSchema>) {
  console.log("onSubmit", event);
  loading.value = true;

  try {
    await subscribe(event.data);
    console.log("Inscription réussie.");
    toast.add({title: 'Inscription réussie.'});
  } catch (error) {
    if (error instanceof z.ZodError) {
      toast.add({title: 'Erreur de validation', description: 'Veuillez vérifier les champs du formulaire.'});
    } else {
      toast.add({title: 'Erreur lors de la soumission', description: 'Veuillez réessayer.'});
    }
  } finally {
    loading.value = false;
  }
}
</script>

<template>
  <UForm :schema="newsletterSchema" :state="state" @submit="onSubmit" class="flex gap-4 w-full">
    <UFormGroup class="w-full max-w-[300px]">
      <UInput v-model="state.email" type="email"
              placeholder="Votre adresse email"
              :ui="inputUI"
              />
    </UFormGroup>
    <UButton
        type="submit"
        label="S'abonner"
        :disabled="loading"
        :loading="loading"
        color="white"
        class="rounded-lg"
        :ui="buttonUI"/>
  </UForm>
</template>

<style scoped lang="scss">

</style>