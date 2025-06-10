using AutoMapper;
using LodgerBackend.App.Settings.Repositories;
using LodgerBackend.src.App.Settings.Dtos;
using LodgerBackend.src.App.Settings.Enums;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace LodgerBackend.App.Settings;

public class SettingsService(ISettingsRepository settingsRepository, ILogger<SettingsService> logger, IMapper mapper) : ISettingsService
{
    public async Task<SettingsDto?> GetSettingsByUserId(int userId)
    {
        try
        {
            var settings = await settingsRepository.GetSettingsByUserId(userId);

            if (settings == null)
            {
                logger.LogWarning("Aucun paramètre trouvé pour l'utilisateur avec ID {UserId}.", userId);
                return null;
            }

            var settingsDto = mapper.Map<SettingsDto>(settings);
            logger.LogInformation("Paramètres récupérés pour l'utilisateur {UserId}.", userId);

            return settingsDto;
        }
        
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération des paramètres pour l'utilisateur {UserId}.", userId);
            throw;
        }
    }
    public async Task<SettingsDto> GetSettingsAsync(int userId)
    {
        try
        {
            var settings = await settingsRepository.GetSettingsByUserIdAsync(userId);
            if (settings == null) return new SettingsDto(); // tout à false;

            var accountSecurity = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.AccountSecurity));
            var email = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.Email));
            var listingUpdates = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.FollowAdvert));
            var sms = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.SMS));
            var push = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.NotificationPush));
            var folderActivity = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.RentalActivity));

            var settingsDto = new SettingsDto
            {
                AccountSecurity = accountSecurity is not null && accountSecurity.NotificationsEnabled,
                Email = email is not null && email.NotificationsEnabled,
                ListingUpdates = listingUpdates is not null && listingUpdates.NotificationsEnabled,
                Sms = sms is not null && sms.NotificationsEnabled,
                Push = push is not null && push.NotificationsEnabled,
                FolderActivity = folderActivity is not null && folderActivity.NotificationsEnabled
            };


            return settingsDto;
        }
        catch
        {
            throw new Exception("Erreur lors de la récupération des paramètres.");
        }
    }
    public async Task<SettingsDto> UpdateSettingsAsync(int userId, UpdateSettingsDto newSettingsDto)
    {
        try
        {

            var settings = await settingsRepository.GetSettingsByUserIdAsync(userId);

            if (settings == null) return new SettingsDto(); // tout à false;

            var accountSecurity = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.AccountSecurity));
            var email = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.Email));
            var listingUpdates = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.FollowAdvert));
            var sms = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.SMS));
            var push = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.NotificationPush));
            var folderActivity = settings.FirstOrDefault(s => s.Theme.Equals(ENotification.RentalActivity));


            if (accountSecurity is null) settings.Add(new Models.Settings(userId, ENotification.AccountSecurity, newSettingsDto.AccountSecurity));
            else accountSecurity.NotificationsEnabled = newSettingsDto.AccountSecurity;

            if (email is null) settings.Add(new Models.Settings(userId, ENotification.Email, newSettingsDto.Email));
            else email.NotificationsEnabled = newSettingsDto.Email;

            if (listingUpdates is null) settings.Add(new Models.Settings(userId, ENotification.FollowAdvert, newSettingsDto.ListingUpdates));
            else listingUpdates.NotificationsEnabled = newSettingsDto.ListingUpdates;

            if (sms is null) settings.Add(new Models.Settings(userId, ENotification.SMS, newSettingsDto.Sms));
            else sms.NotificationsEnabled = newSettingsDto.Sms;

            if (push is null) settings.Add(new Models.Settings(userId, ENotification.NotificationPush, newSettingsDto.Push));
            else push.NotificationsEnabled = newSettingsDto.Push;

            if (folderActivity is null) settings.Add(new Models.Settings(userId, ENotification.RentalActivity, newSettingsDto.FolderActivity));
            else folderActivity.NotificationsEnabled = newSettingsDto.FolderActivity;



            // Sauvegarder les changements dans la base de données
            await settingsRepository.UpdateSettingsAsync(settings);

            var settingsDto = await GetSettingsAsync(userId);

            return settingsDto;
        }
        catch
        {
            throw new Exception("Erreur lors de la mise à jour des paramètres.");
        }
    }

}