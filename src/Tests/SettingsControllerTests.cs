using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Setting;
using System.Threading.Tasks;
using LodgerBackend.Setting.Dtos;

namespace Tests;

public class SettingsControllerTests
{
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<ISettingsService> _settingsService = new();

    private SettingsController CreateController()
    {
        return new SettingsController(_currentUserService.Object, _settingsService.Object);
    }

    [Fact]
    public async Task GetSettings_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);

        var controller = CreateController();
        var result = await controller.GetSettings();

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Utilisateur non connecté.", unauthorized.Value);
    }

    [Fact]
    public async Task GetSettings_ReturnsNotFound_WhenSettingsIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        _settingsService.Setup(s => s.GetSettingsByUserId(1)).ReturnsAsync((SettingsDto?)null); 

        var controller = CreateController();
        var result = await controller.GetSettings();

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Paramètre non trouvés.", notFound.Value);
    }

    [Fact]
    public async Task GetSettings_ReturnsOk_WhenSettingsFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var settings = new SettingsDto 
        {
            Push = true,
            Email = false,
            Sms = true,
            FolderActivity = false,
            ListingUpdates = true,
            AccountSecurity = false
        };
        _settingsService.Setup(s => s.GetSettingsByUserId(1)).ReturnsAsync(settings);

        var controller = CreateController();
        var result = await controller.GetSettings();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(settings, ok.Value);
    }

    [Fact]
    public async Task GetSettings_ReturnsServerError_OnException()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        _settingsService.Setup(s => s.GetSettingsByUserId(1)).ThrowsAsync(new System.Exception("fail"));

        var controller = CreateController();
        var result = await controller.GetSettings();

        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);
        Assert.Equal("Une erreur est survenue lors de la récupération des paramètres.", error.Value);
    }
}
