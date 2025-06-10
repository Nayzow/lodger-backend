using LodgerBackend.App.Configuration.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.Settings.Repositories;

public class SettingsRepository(LodgerDbContext dbContext) : ISettingsRepository
{
    public async Task<Models.Settings?> GetSettingsByUserId(int userId)
    {
        return await dbContext.Settings.FirstOrDefaultAsync(settings => settings != null && settings.UserId == userId);
    }

    public async Task<List<Models.Settings>> GetSettingsByUserIdAsync(int userId)
    {
        return await dbContext.Settings
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
    public async Task UpdateSettingsAsync(List<Models.Settings> settings)
    {
        foreach (var setting in settings)
        {
            if (setting.Id == 0) dbContext.Settings.Add(setting);
            else dbContext.Settings.Update(setting);
        }
        await dbContext.SaveChangesAsync();
    }

}