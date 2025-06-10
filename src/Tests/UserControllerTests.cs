using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LodgerBackend.User;
using LodgerBackend.User.Services;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Device.Services;
using LodgerBackend.Document.Services;
using LodgerBackend.RentalFile.Services;
using LodgerBackend.Setting;
using LodgerBackend.Setting.Dtos;
using LodgerBackend.User.Models.Dtos;
using LodgerBackend.User.PDF;
using LodgerBackend.File.Models;
using LodgerBackend.RentalFile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using LodgerBackend.File.dtos;
using LodgerBackend.Document.Models;

namespace Tests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userService = new();
    private readonly Mock<IPasswordResetService> _passwordResetService = new();
    private readonly Mock<IDeviceService> _deviceService = new();
    private readonly Mock<IDocumentService> _documentService = new();
    private readonly Mock<IRentalFileService> _rentalFileService = new();
    private readonly Mock<ISettingsService> _settingsService = new();
    private readonly Mock<ICurrentUserService> _currentUserService = new();

    private UserController CreateController()
    {
        return new UserController(
            _userService.Object,
            _passwordResetService.Object,
            _deviceService.Object,
            _documentService.Object,
            _rentalFileService.Object,
            _settingsService.Object,
            _currentUserService.Object
        );
    }

    [Fact]
    public async Task GetUserWithDetailsAsync_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);

        var controller = CreateController();
        var result = await controller.GetUserWithDetailsAsync();

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task GetUserWithDetailsAsync_ReturnsOk_WhenUserFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var userDetails = new UserDetailsDto
        {
            Name = "Test",
            FirstName = "User",
            Email = "test@test.fr",
            Phone = "0600000000",
            GenderName = "M",
            NationalityName = "FR",
            Birthday = null,
            AddressName = "1 rue",
            PostalCode = "75000",
            IdentityFile = null
        };
        _userService.Setup(s => s.GetUserWithDetailsAsync(1)).ReturnsAsync(userDetails);

        var controller = CreateController();
        var result = await controller.GetUserWithDetailsAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(userDetails, ok.Value);
    }

    [Fact]
    public async Task UpdateUserWithDetailsAsync_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.UpdateUserWithDetailsAsync(new UserDetailsDto
        {
            Name = "Test",
            FirstName = "User",
            Email = "test@test.fr",
            Phone = "0600000000",
            GenderName = "M",
            NationalityName = "FR",
            Birthday = null,
            AddressName = "1 rue",
            PostalCode = "75000",
            IdentityFile = null
        });

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task ChangePassword_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.ChangePassword(new ChangePasswordDto
        {
            Password = "old",
            NewPassword = "new",
            ConfirmNewPassword = "new"
        });

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Utilisateur non authentifié.", unauthorized.Value);
    }

    [Fact]
    public async Task GetUserDevicesAsync_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.GetUserDevicesAsync();

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task GetUserDevicesAsync_ReturnsOk_WhenDevicesFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var devices = new List<LodgerBackend.Device.Models.DeviceDto>
       {
           new() { Id = 1, Type = "PC", Location = "Salon", Date = "2023-10-01" }
       };
        _deviceService.Setup(s => s.GetDevicesByUserId(1)).ReturnsAsync(devices);

        var controller = CreateController();
        var result = await controller.GetUserDevicesAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(devices, ok.Value);
    }

    [Fact]
    public async Task DeleteDevice_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.DeleteDevice(1);

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task DeleteDevice_ReturnsOk_WhenSuccess()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        _deviceService.Setup(s => s.DeleteDeviceId(1, 2)).ReturnsAsync((true, "ok"));

        var controller = CreateController();
        var result = await controller.DeleteDevice(2);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("ok", ok.Value);
    }

    [Fact]
    public async Task Upload_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.Upload(new UploadFilesRequest { Files = new List<UploadFileDto>() });

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task Upload_ReturnsOk_WhenSuccess()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        _documentService.Setup(s => s.UploadFilesAsync(1, It.IsAny<List<UploadFileDto>>()))
            .ReturnsAsync((true, "ok"));

        var controller = CreateController();
        var result = await controller.Upload(new UploadFilesRequest { Files = new List<UploadFileDto>() });

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("ok", ok.Value);
    }

    [Fact]
    public async Task Download_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.Download("file.pdf");

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task Download_ReturnsFileStreamResult_WhenSuccess()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var fileStreamResult = new FileStreamResult(new MemoryStream(), "application/pdf");
        _documentService.Setup(s => s.DownloadFile("file.pdf", 1)).ReturnsAsync(fileStreamResult);

        var controller = CreateController();
        var result = await controller.Download("file.pdf");

        Assert.IsType<FileStreamResult>(result);
    }

    [Fact]
    public async Task GetRentalFile_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.GetRentalFile();

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task GetRentalFile_ReturnsOk_WhenRentalFileFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var rentalFileDto = new RentalFileDto
        {
            ProStatus = "CDI",
            MonthlyIncome = "2000",
            HasGuarantor = "oui",
            GuarantorIncome = "3000",
            StatusFamilial = "Célibataire",
            RoomatesNb = "0",
            HasAnimals = "non",
            Smoker = "non",
            Files = new List<DocumentDto>()
        };
        _rentalFileService.Setup(s => s.GetRentalFileDetailsByUserId(1)).ReturnsAsync(rentalFileDto);

        var controller = CreateController();
        var result = await controller.GetRentalFile();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(rentalFileDto, ok.Value);
    }

    [Fact]
    public async Task UpdateRentalFile_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.UpdateRentalFile(new UpdateRentalFileDto
        {
            ProStatus = "CDI",
            MonthlyIncome = "2000",
            HasGuarantor = "oui",
            GuarantorIncome = "3000",
            StatusFamilial = "Célibataire",
            RoomatesNb = "0",
            HasAnimals = "non",
            Smoker = "non",
            Files = new List<UploadFileDto>()
        });

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task ExportUserDataAsPdf_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.ExportUserDataAsPdf();

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task DeleteCurrentUserAsync_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.DeleteCurrentUserAsync();

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task GetSettingsAsync_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.GetSettingsAsync();

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task UpdateSettingsAsync_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.UpdateSettingsAsync(new UpdateSettingsDto());

        Assert.IsType<UnauthorizedResult>(result);
    }
}
