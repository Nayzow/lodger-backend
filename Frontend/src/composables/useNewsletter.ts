import type {ApiInstance} from "~/plugins/api";

export const useNewsletter = () => {
    const {$api} = useNuxtApp();
    const newsletterModule = ($api as ApiInstance).newsletter;

    async function subscribe({email}: {email: string}) {
        return newsletterModule.subscribe({email});
    }

    return {subscribe};
}