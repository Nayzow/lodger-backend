import type {RegisterSchema} from "~/models/schemas/auth/register";
import type {LoginSchema} from "~/models/schemas/auth/login";
import type {ApiInstance} from "~/plugins/api";


export const useAuth = () => {
    const {$api} = useNuxtApp();
    const authModule = ($api as ApiInstance).auth;

    const register = (data: RegisterSchema) => {
        return authModule.create(data);
    };

    const login = async (data: LoginSchema) => {
        const {accessToken} = await authModule.login(data);
        const authStore = useAuthStore();
        authStore.login(accessToken);
    }

    const socialSignIn = async (provider: any, user: any) => {
        const {accessToken} = await authModule.socialSignIn(provider, user);
        const authStore = useAuthStore();
        authStore.login(accessToken);
    }

    return {register, login, socialSignIn};
};
