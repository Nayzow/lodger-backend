using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LodgerBackend.Device.Services;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Device;

namespace Tests;

public class DeviceControllerTests
{
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<IDeviceService> _deviceService = new();

    private DeviceController CreateController()
    {
        return new DeviceController(_currentUserService.Object, _deviceService.Object);
    }

    [Fact]
    public async Task GetDevices_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);

        var controller = CreateController();
        var result = await controller.GetDevices();

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Utilisateur non connecté.", unauthorized.Value);
    }

    [Fact]
    public async Task GetDevices_ReturnsOk_WhenDevicesFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(42);
        var devices = new List<LodgerBackend.Device.Models.DeviceDto> // Corrected namespace  
       {
           new() { Id = 1, Type = "PC", Location = "Salon", Date = "2024-06-01" }
       };
        _deviceService.Setup(s => s.GetDevicesByUserId(42)).Returns(Task.FromResult(devices));

        var controller = CreateController();
        var result = await controller.GetDevices();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(devices, okResult.Value);
    }

    [Fact]
    public async Task GetDevices_ReturnsServerError_OnException()
    {
        _currentUserService.Setup(s => s.userId).Returns(42);
        _deviceService.Setup(s => s.GetDevicesByUserId(42)).ThrowsAsync(new System.Exception("fail"));

        var controller = CreateController();
        var result = await controller.GetDevices();

        var errorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, errorResult.StatusCode);
        Assert.Equal("Une erreur est survenue lors de la récupération des paiements.", errorResult.Value);
    }
}
