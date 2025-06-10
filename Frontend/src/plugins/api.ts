import { defineNuxtPlugin } from '#app';
import AuthModule from "~/repository/modules/auth";
import LogementModule from "~/repository/modules/logement";
import NewsletterModule from "~/repository/modules/newsletter";

export interface ApiInstance {
    baseURL: string
    auth: AuthModule;
    logements: LogementModule;
    newsletter: NewsletterModule;
}

export default defineNuxtPlugin((nuxtApp) => {
    const baseURL = nuxtApp.$config.public.API_URL;
    const fetchOptions: any = {
        baseURL,
        headers: {
            'Content-Type': 'application/json',
        },
    }

    const apiFetcher = $fetch.create(fetchOptions);

    const authModule = new AuthModule(apiFetcher);
    const logementModule = new LogementModule(apiFetcher);
    const newsletterModule = new NewsletterModule(apiFetcher);


    return {
        provide: {
            api: {
                baseURL: baseURL,
                auth: authModule,
                logements: logementModule,
                newsletter: newsletterModule,
            } as ApiInstance,
        },
    };
})