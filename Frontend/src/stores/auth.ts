import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
    state: () => ({
        isAuth: false,
    }),
    actions: {
        login(accessToken: string) {
            const cookie = useCookie('accessToken', {
                path: '/',
                sameSite: true,
                secure: true,
                maxAge: 60 * 60 // 1 heure en secondes (refreshToken ??)
            });
            cookie.value = accessToken
            this.isAuth = !!accessToken;
        },
        logout() {
            this.isAuth = false;
            const cookie = useCookie('accessToken');
            cookie.value = null;
        },
        checkAuth() {
            const cookie = useCookie('accessToken');
            this.isAuth = !!cookie.value;
            return this.isAuth;
        },
    },
    persist: true,
});
