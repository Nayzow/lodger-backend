using LodgerBackend.Setting.Dtos;

namespace LodgerBackend.Setting;

public interface ISettingsService
{
    Task<SettingsDto?> GetSettingsByUserId(int userId);
    Task<SettingsDto> GetSettingsAsync(int userId);
    Task<SettingsDto> UpdateSettingsAsync(int userId, UpdateSettingsDto newSettingsDto);
}