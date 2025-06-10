import type {ProfileInfoDto} from "~/models/dto/profile-info-dto";
import {FetchError} from "ofetch";
import {HttpMethod} from "~/models/enums/httpMethod";
import {PROFILE_URL} from "~/utils/config/api/endpoints";
import type {ProfileAvatarDto} from "~/models/dto/profile-avatar-dto";

export const useProfile = () => {
    const {$api} = useNuxtApp();

    // const profileModule = ($api as ApiInstance).profile;

    async function getProfile(): Promise<{
        data: Ref<ProfileInfoDto | null>,
        status: Ref<string>,
        error: Ref<FetchError<any> | null>
    }> {
        const {data, status, error} = await useApiFetch<ProfileInfoDto>(PROFILE_URL, {method: HttpMethod.GET});

        return {data, status, error};
    }

    async function getAvatar(): Promise<{
        data: Ref<ProfileAvatarDto | null>,
        status: Ref<string>,
        error: Ref<FetchError<any> | null>
    }> {
        const {data, status, error} = await useApiFetch<ProfileAvatarDto>(PROFILE_URL + '/avatar', {method: HttpMethod.GET});

        return {data, status, error};
    }

    async function updateProfile(profile: ProfileInfoDto): Promise<{
        data: Ref<ProfileInfoDto | null>,
        status: Ref<string>,
        error: Ref<FetchError<any> | null>
    }> {
        const {data, status, error} = await useApiFetch<ProfileInfoDto>(PROFILE_URL, {
            method: HttpMethod.PUT,
            body: profile,
        });

        return {data, status, error};
    }

    async function updateAvatar(avatar: File): Promise<{
        data: Ref<ProfileAvatarDto | null>,
        status: Ref<string>,
        error: Ref<FetchError<any> | null>
    }> {
        const {data, status, error} = await useApiFetch<ProfileAvatarDto>(PROFILE_URL + '/avatar', {
            method: HttpMethod.PUT,
            body: avatar,
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });

        return {data, status, error};
    }

    return {getProfile, getAvatar, updateProfile, updateAvatar};
}