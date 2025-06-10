import {z} from "zod";

export const advancedSearchSchema = z.object({
    type: z.string().optional(),
    localisation: z.string(),
    prixMin: z.number().int().nonnegative().optional(),
    prixMax: z.number().int().nonnegative().optional(),
    surfaceHabitableMin: z.number().int().nonnegative().optional(),
    surfaceHabitableMax: z.number().int().nonnegative().optional(),
    surfaceTerrainMin: z.number().int().nonnegative().optional(),
    surfaceTerrainMax: z.number().int().nonnegative().optional(),
    nombreDePieces: z.string().optional(),
    nombreDeChambres: z.string().optional(),
    motsCles: z.array(z.string()),
    chauffage: z.string().optional(),
    dpo: z.string().optional(),
    strict: z.boolean().optional(),
});


export type AdvancedSearchSchema = z.infer<typeof advancedSearchSchema>;