using System.Net;
using AutoMapper;
using LodgerBackend.Device.Models;
using LodgerBackend.Device.Repositories;

namespace LodgerBackend.Device.Services;

public class DeviceService(IDeviceRepository deviceRepository,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : IDeviceService
{
    public async Task<Models.Device?> GetDeviceByIpAddress(int userId, string ipAddress)
    {
        return await deviceRepository.GetDeviceByIpAddress(userId, ipAddress);
    }

    public async Task<List<DeviceDto>> GetDevicesByUserId(int userId)
    {
        var devices = await deviceRepository.GetDevicesByUseId(userId);
        var devicesDto = mapper.Map<List<DeviceDto>>(devices);
        
        return devicesDto;
    }
    public async Task<Device.Models.Device?> GetDeviceWithIpAddress(int userId)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null) return null;
        // 1. Adresse IP
        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
        if (ipAddress is null) return null;

        if (ipAddress == "::1")
        {
            ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
        }

        var device = await GetDeviceByIpAddress(userId, ipAddress);
        if (device is not null) return device;


        // 2. User-Agent (navigateur, mobile, etc)
        var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
        device = new Models.Device
        {
            UserId = userId,
            Type = userAgent,
            IpAddress = ipAddress,
            Name = ipAddress,
            Location = "France",
            Date = DateTime.UtcNow
        };

        device = await deviceRepository.Save(device);
        return device;
    }

    public async Task<(bool success, string message)> DeleteDeviceId(int userId, int deviceId)
    {
        try
        {
            var device = await deviceRepository.GetDeviceByUserIdAndDeviceId(userId, deviceId);
            if (device is null) return (false, "L'appareil n'est pas disponible.");

            await deviceRepository.DeleteDeviceById(deviceId);
            return (true,  $"{deviceId} supprimé.");
        }
        catch (Exception)
        {

            return (false, "Erreur lors de la suppresion de l'appareil.");

        }
    }
    public async Task<Device.Models.Device> AddDeviceInsecure(int userId, string? type, string? ipAddress, string? location, string? dateString)
    {
        // Aucune validation sur les champs, ni gestion d'erreur sur la date
        var device = new Device.Models.Device
        {
            UserId = userId,
            Type = type, // Peut être null ou vide
            IpAddress = ipAddress, // Peut être null ou vide
            Name = ipAddress,
            Location = location, // Peut être null ou vide
            Date = DateTime.Parse(dateString) // Peut lever une exception si dateString est null ou mal formé
        };

        return await deviceRepository.Save(device);
    }
}