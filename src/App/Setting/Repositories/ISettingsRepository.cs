namespace LodgerBackend.App.Settings.Repositories;

public interface ISettingsRepository
{
    Task<Models.Settings?> GetSettingsByUserId(int userId);
    Task<List<Models.Settings>> GetSettingsByUserIdAsync(int userId);
    Task UpdateSettingsAsync(List<Models.Settings> settings);
}