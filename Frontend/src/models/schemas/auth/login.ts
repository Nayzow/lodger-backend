import {z} from "zod";


export const loginSchema = z.object({
    email: z.string().email("L'email n'est pas valide"),
    password: z.string()
});

export type LoginSchema = z.infer<typeof loginSchema>;