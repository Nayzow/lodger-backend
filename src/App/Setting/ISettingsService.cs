using LodgerBackend.src.App.Settings.Dtos;

namespace LodgerBackend.App.Settings;

public interface ISettingsService
{
    Task<SettingsDto?> GetSettingsByUserId(int userId);
    Task<SettingsDto> GetSettingsAsync(int userId);
    Task<SettingsDto> UpdateSettingsAsync(int userId, UpdateSettingsDto newSettingsDto);
}