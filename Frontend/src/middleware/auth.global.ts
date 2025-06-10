import {jwtDecode} from "jwt-decode";


export default defineNuxtRouteMiddleware((to, from) => {
    if (import.meta.client) return

    const cookie = useCookie('accessToken');
    const accessToken = cookie.value;


    if (accessToken) {
        try {
            const decodedToken = jwtDecode(accessToken);
            const currentTime = Date.now() / 1000;

            if (decodedToken.exp && decodedToken.exp < currentTime) {
                cookie.value = '';
            }

        } catch (error) {
            cookie.value = '';
        }
    }
});
