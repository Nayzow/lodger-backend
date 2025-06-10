<script setup lang="ts">
import type {FormSubmitEvent} from '#ui/types'
import {registerSchema, type RegisterSchema} from "~/models/schemas/auth/register";
import {ref, reactive} from 'vue';
import {z} from 'zod';
import {genderOptions} from "~/utils/config/options/options";

const {register} = useAuth()
const toast = useToast()

const loading = ref(false);

const state = reactive<RegisterSchema>({
  genre: '',
  nom: '',
  prenom: '',
  email: '',
  password: '',
  telephone: '',
  dateNaissance: '',
  consentement: false
});

const toggleFormType = inject<() => void>('toggleFormType', () => {});

async function onSubmit(event: FormSubmitEvent<RegisterSchema>) {
  loading.value = true;

  try {
    await register(event.data);
    toast.add({title: 'Inscription réussie. Veuillez vérifier votre boîte de réception pour confirmer votre adresse e-mail.'});
    toggleFormType();
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
    <p class="text-2xl text-secondaire font-semibold">Inscription</p>
    <UForm :schema="registerSchema" :state="state" class="space-y-4 gap-4" @submit="onSubmit">
      <UFormGroup label="Genre" name="genre">
        <div class="flex gap-4">
          <div v-for="(option, i) in genderOptions" :key="option.value" class="flex items-center gap-4">
            <URadio v-model="state.genre" :value="option.value" :id="'genre' + i"/>
            <label :for="'genre' + i">{{ option.label }}</label>
          </div>
        </div>
      </UFormGroup>
      <div class="flex flex-col gap-4 md:flex-row">
        <UFormGroup label="Nom" name="nom" class="flex-1">
          <UInput v-model="state.nom" size="lg"/>
        </UFormGroup>

        <UFormGroup label="Prénom" name="prenom" class="flex-1">
          <UInput v-model="state.prenom" size="lg"/>
        </UFormGroup>
      </div>
      <div class="flex flex-col gap-4 md:flex-row">
        <UFormGroup label="Email" name="email" class="flex-1">
          <UInput v-model="state.email" autocomplete size="lg"/>
        </UFormGroup>

        <UFormGroup label="Mot de passe" name="password" class="flex-1">
          <UInput v-model="state.password" type="password" autocomplete="current-password" size="lg"/>
        </UFormGroup>
      </div>
      <div class="flex flex-col gap-4 md:flex-row">
        <UFormGroup label="Téléphone" name="telephone" class="flex-1">
          <UInput v-model="state.telephone" size="lg"/>
        </UFormGroup>

        <UFormGroup label="Date de naissance" name="dateNaissance" class="flex-1">
          <UInput v-model="state.dateNaissance" type="date" size="lg"/>
        </UFormGroup>
      </div>
      <div class="flex flex-col gap-4">
        <p class="text-gray-400 text-sm">
          Nous vous enverrons un courriel à l’adresse renseigner pour confirmer votre adresse e-mail.
        </p>
        <UCheckbox
            v-model="state.consentement"
            name="conditions"
            label="J'accepte les conditions d'utilisation et la politique de confidentialité"
        />
        <NuxtLink to="/src/public#" class="underline">Les Politique de confidentialité</NuxtLink>
      </div>
      <div class="flex flex-col md:flex-row gap-4">
        <UButton
            type="submit"
            class="flex justify-center items-center bg-principal hover:bg-principal/80 text-white px-4 py-2 rounded-lg"
            :disabled="loading"
            :loading="loading"
        >
          S'inscrire
        </UButton>
        <UButton
            class="flex justify-center items-center bg-secondaire text-white  px-4 py-2 rounded-lg hover:bg-secondaire/80"
            @click="toggleFormType()"
        >
          Déjà inscrit ? Connectez-vous
        </UButton>
      </div>
    </UForm>
</template>
