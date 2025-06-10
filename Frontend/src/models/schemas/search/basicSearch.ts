import {z} from "zod";

export const basicSearchSchema = z.object({
    type: z.string().regex(/^[a-zA-Z0-9_\-, ]+$/, 'Format de type invalide'),
    localisation: z.string(),
    prixMin: z.number().int().min(0, 'Le prix minimum doit être supérieur ou égal à 0').optional(),
    prixMax: z.number().int().min(0, 'Le prix maximum doit être supérieur ou égal à 0').optional(),
});

export type BasicSearchSchema = z.infer<typeof basicSearchSchema>;