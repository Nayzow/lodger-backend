export default defineNuxtPlugin((nuxtApp) => {
    const config = useRuntimeConfig();
    let backendURL = "";
    if (import.meta.server) {
        backendURL = config.public.API_URL_SERVER;
    } else {
        backendURL = config.public.API_URL;
    }
    return {
        provide: {
            backendURL: backendURL
        }
    };
});