
import {FetchError} from "ofetch";
import {HttpMethod} from "~/models/enums/httpMethod";
import {NOTIFICATION_URL, CHANGE_PASSWORD_URL} from "~/utils/config/api/endpoints";
import type {UpdateNotificationSettingsDto} from "~/models/dto/update-notification-settings-dto";
import type {ChangePasswordDto} from "~/models/dto/change-password-dto";

export const useSettings = () => {
    const {$api} = useNuxtApp();

    async function getNotificationSettings() {
        return await useApiFetch<UpdateNotificationSettingsDto>(NOTIFICATION_URL + '/notifications', {method: HttpMethod.GET});
    }

    async function updateNotificationSettings(settings: UpdateNotificationSettingsDto) {
        return await $fetch<UpdateNotificationSettingsDto>(NOTIFICATION_URL, {
            method: HttpMethod.PUT,
            body: settings,
        }).then((data) => {
            return {data, status: 'success', error: null};
        }).catch((error: FetchError<any>) => {
            return {data: null, status: 'error', error};
        });
    }

    async function changePassword(dto: ChangePasswordDto) {
        return await $fetch(CHANGE_PASSWORD_URL, {
            method: HttpMethod.POST,
            body: dto,
        }).then((data) => {
            return {data, status: 'success', error: null};
        }).catch((error: FetchError<any>) => {
            return {data: null, status: 'error', error};
        });
    }

    return {getNotificationSettings, updateNotificationSettings, changePassword};
};