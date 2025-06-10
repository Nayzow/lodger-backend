import type {ProfileInfoDto} from "~/models/dto/profile-info-dto";
import {useProfile} from "~/composables/useProfile";
import type {ProfileAvatarDto} from "~/models/dto/profile-avatar-dto";

export const useProfileStore = defineStore('profile', {
    state: (): { profile: ProfileInfoDto | null, avatar: ProfileAvatarDto | null} => ({
        profile: null,
        avatar: null,
    }),
    actions: {
        async getProfile() {
            const {data, status, error} = await useProfile().getProfile();
            this.setProfile(data.value);

        },
        async getAvatar() {
            const {data, status, error} = await useProfile().getAvatar();
            this.setAvatar(data.value);
        },
        async updateProfile(profile: ProfileInfoDto) {
            const {data, status, error} = await useProfile().updateProfile(profile);
            this.setProfile(data.value);
        },
        async updateAvatar(avatar: File) {
            const {data, status, error} = await useProfile().updateAvatar(avatar);
            this.setAvatar(data.value);
        },
        setProfile(profile: ProfileInfoDto | null) {
            this.profile = profile;
        },
        setAvatar(avatar: ProfileAvatarDto | null) {
            this.avatar = avatar;
        },
    },
    persist: true,
})