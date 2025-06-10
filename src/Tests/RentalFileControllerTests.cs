using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.RentalFile.Services;
using LodgerBackend.RentalFile;
using LodgerBackend.RentalFile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using LodgerBackend.Document.Models;

namespace Tests;

public class RentalFileControllerTests
{
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<IRentalFileService> _rentalFileService = new();

    private RentalFileController CreateController()
    {
        return new RentalFileController(_currentUserService.Object, _rentalFileService.Object);
    }

    [Fact]
    public async Task GetRentalFile_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);

        var controller = CreateController();
        var result = await controller.GetRentalFile();

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result.Result);
        Assert.Equal("Utilisateur non connecté", unauthorized.Value);
    }

    [Fact]
    public async Task GetRentalFile_ReturnsNotFound_WhenRentalFileIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns(10);
        _rentalFileService.Setup(s => s.GetRentalFileDetailsByUserId(10)).ReturnsAsync((RentalFileDto?)null);

        var controller = CreateController();
        var result = await controller.GetRentalFile();

        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("Aucun dossier de location trouvé pour l'utilisateur 10", notFound.Value);
    }

    [Fact]
    public async Task GetRentalFile_ReturnsOk_WhenRentalFileFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(10);
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
        _rentalFileService.Setup(s => s.GetRentalFileDetailsByUserId(10)).ReturnsAsync(rentalFileDto);

        var controller = CreateController();
        var result = await controller.GetRentalFile();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(rentalFileDto, ok.Value);
    }

    [Fact]
    public async Task GetRentalFile_ReturnsServerError_OnException()
    {
        _currentUserService.Setup(s => s.userId).Returns(10);
        _rentalFileService.Setup(s => s.GetRentalFileDetailsByUserId(10)).ThrowsAsync(new System.Exception("fail"));

        var controller = CreateController();
        var result = await controller.GetRentalFile();

        var error = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, error.StatusCode);
        Assert.NotNull(error.Value);
        Assert.Contains("Une erreur s'est produite lors de la récupération du dossier", error.Value.ToString());
    }

    // Test qui échoue volairement
    [Fact]
    public void FailingTest()
    {
        Assert.True(false, "Ce test est censé échouer volontairement");
    }
}
