import {z} from "zod";
import {passwordStrengthCheck} from "~/utils/helpers/validateUtils";


export const registerSchema = z.object({
    genre: z.string().min(1, "Le genre est obligatoire"),
    nom: z.string().min(1, "Le nom est obligatoire"),
    prenom: z.string().min(1, "Le prénom est obligatoire"),
    email: z.string().email("L'adresse email est invalide"),
    password: z.string()
        .min(8, "Le mot de passe doit contenir au moins 8 caractères")
        .refine(passwordStrengthCheck, "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre et un caractère spécial"),
    telephone: z.string().min(10, "Le numéro de téléphone doit contenir au moins 10 chiffres"),
    dateNaissance: z.string().min(10, "La date de naissance est obligatoire"),
    consentement: z.boolean().refine(value => value, "Vous devez accepter les conditions d'utilisation")
});

export type RegisterSchema = z.infer<typeof registerSchema>;