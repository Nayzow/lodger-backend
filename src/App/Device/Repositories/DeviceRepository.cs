using LodgerBackend.App.Auth.Services.Implementations;
using LodgerBackend.App.Configuration.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LodgerBackend.App.Device.Repositories;

public class DeviceRepository(LodgerDbContext dbContext) : IDeviceRepository
{
    public async Task<Models.Device?> GetDeviceByIpAddress(int userId, string ipAddress)
    {
        return await dbContext.Devices
            .Where(x => x.IpAddress == ipAddress && x.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteDeviceById(int deviceId)
    {
        await dbContext.Devices
            .Where(d => d.Id == deviceId)
            .ExecuteDeleteAsync();
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Models.Device>> GetDevicesByUseId(int userId)
    {
        return await dbContext.Devices
            .Where(device => device.UserId == userId)
            .ToListAsync();
    }

    public async Task<Models.Device> Save(Models.Device device)
    {
        dbContext.Devices.Add(device);
        await dbContext.SaveChangesAsync();

        return device;
    }

    public async Task<Models.Device?> GetDeviceByUserIdAndDeviceId(int userId, int deviceId)
    {
        return await dbContext.Devices
            .Where(d => d.UserId == userId && d.Id == deviceId)
            .FirstOrDefaultAsync();
    }
}