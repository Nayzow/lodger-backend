

namespace LodgerBackend.Device.Services;

public interface IDeviceService
{
    Task<(bool success, string message)> DeleteDeviceId(int userId, int deviceId);
    Task<Models.Device?> GetDeviceWithIpAddress(int userId);
    Task<Models.Device?> GetDeviceByIpAddress(int userId, string ipAddress);
    Task<List<Models.DeviceDto>> GetDevicesByUserId(int userId);
}