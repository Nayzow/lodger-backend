<script setup lang="ts">
import type {FormSubmitEvent} from '#ui/types'
import {type LoginSchema, loginSchema} from "~/models/schemas/auth/login";
import { useAuth as useCustomUseAuth } from '~/composables/useAuth';
import {z} from "zod";

const {login} = useCustomUseAuth();
const { signIn } = useAuth()

const toast = useToast();

const loading = ref(false);

const state = reactive<LoginSchema>({
  email: '',
  password: ''
})

const toggleModal = inject<() => void>('toggleModal', () => {});
const toggleFormType = inject<() => void>('toggleFormType', () => {});

async function onSubmit(event: FormSubmitEvent<LoginSchema>) {
  loading.value = true;

  try {
    await login(event.data);
    toast.add({title: 'Connexion réussie.'});
    toggleModal();
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
  <h2 class="text-2xl text-secondaire font-semibold">Connexion</h2>
  <UForm :schema="loginSchema" :state="state" class="space-y-4 gap-4 w-full" @submit="onSubmit">
    <UFormGroup label="Email" name="email">
      <UInput v-model="state.email" auto-complete size="lg"/>
    </UFormGroup>
    <UFormGroup label="Mot de passe" name="password">
      <UInput v-model="state.password" type="password" autocomplete="current-password" size="lg"/>
    </UFormGroup>
    <div class="flex flex-col gap-4">
      <NuxtLink to="/src/public#" class="text-secondaire text-sm font-semibold underline">Mot de passe oublié ?</NuxtLink>
    </div>
    <div class="flex flex-col md:flex-row gap-4">
      <UButton
          type="submit"
          class="flex justify-center items-center bg-principal hover:bg-principal/80 text-white px-4 py-2 rounded-lg"
          :disabled="loading"
          :loading="loading"
      >
        Connexion
      </UButton>
      <UButton
          class="flex justify-center items-center bg-secondaire text-white  px-4 py-2 rounded-lg hover:bg-secondaire/80"
          @click="toggleFormType()"
      >
        Pas encore inscrit ? Créez un compte
      </UButton>
    </div>
  </UForm>
</template>