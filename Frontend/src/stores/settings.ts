import type {UpdateNotificationSettingsDto, ChangePasswordDto} from "~/models/dto/settings-dto";
import {useSettings} from "~/composables/useSettings";

export const useSettingsStore = defineStore('settings', {
    state: (): { notificationSettings: UpdateNotificationSettingsDto | null } => ({
        notificationSettings: null,
    }),
    actions: {
        async getNotificationSettings() {
            const {data} = await useSettings().getNotificationSettings();
            this.setNotificationSettings(data.value);
        },
        async updateNotificationSettings(settings: UpdateNotificationSettingsDto) {
            const {data} = await useSettings().updateNotificationSettings(settings);
            this.setNotificationSettings(data.value);
        },
        async changePassword(dto: ChangePasswordDto) {
            await useSettings().changePassword(dto);
        },
        setNotificationSettings(settings: UpdateNotificationSettingsDto | null) {
            this.notificationSettings = settings;
        },
    },
    persist: true,
});