

namespace LodgerBackend.Device.Repositories;

public interface IDeviceRepository
{
    Task<Models.Device?> GetDeviceByIpAddress(int userId, string ipAddress);
    Task DeleteDeviceById(int deviceId);
    Task<Models.Device?> GetDeviceByUserIdAndDeviceId(int userId, int deviceId);
    Task<List<Models.Device>> GetDevicesByUseId(int userId);
    Task<Models.Device> Save(Models.Device device);
}