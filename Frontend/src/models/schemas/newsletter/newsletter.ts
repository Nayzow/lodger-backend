import {z} from "zod";


export const newsletterSchema = z.object({
    email: z.string().min(1, "L'email ne peut pas être vide").email("L'email n'est pas valide"),
});

export type NewsletterSchema = z.infer<typeof newsletterSchema>;