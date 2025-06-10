export interface UpdateNotificationSettingsDto {
    push: boolean,
    email: boolean,
    sms: boolean,
    folderActivity: boolean,
    listingUpdates: boolean,
    accountSecurity: boolean
}