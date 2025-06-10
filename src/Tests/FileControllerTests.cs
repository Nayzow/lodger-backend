using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.File.Services;
using LodgerBackend.File;
using LodgerBackend.File.dtos;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Tests;

public class FileControllerTests
{
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<IFileService> _fileService = new();

    private FileController CreateController()
    {
        return new FileController(_currentUserService.Object, _fileService.Object);
    }

    [Fact]
    public async Task Upload_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();
        var dto = new UploadFileDto
        {
            File = Mock.Of<IFormFile>(),
            DocumentCategorie = "cat",
            DocumentType = "type"
        };

        var result = await controller.Upload(dto);

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Utilisateur non connecté.", unauthorized.Value);
    }

    [Fact]
    public async Task Upload_ReturnsOk_WhenUploadSucceeds()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var dto = new UploadFileDto
        {
            File = Mock.Of<IFormFile>(),
            DocumentCategorie = "cat",
            DocumentType = "type"
        };
        _fileService.Setup(s => s.UploadFileAsync(dto, 1)).Returns(Task.CompletedTask);

        var controller = CreateController();
        var result = await controller.Upload(dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Fichier uploadé avec succès.", ok.Value);
    }

    [Fact]
    public async Task Upload_ReturnsServerError_OnException()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var dto = new UploadFileDto
        {
            File = Mock.Of<IFormFile>(),
            DocumentCategorie = "cat",
            DocumentType = "type"
        };
        _fileService.Setup(s => s.UploadFileAsync(dto, 1)).ThrowsAsync(new System.Exception("fail"));

        var controller = CreateController();
        var result = await controller.Upload(dto);

        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);
        Assert.Equal("Une erreur est survenue lors de l'upload.", error.Value);
    }

    [Fact]
    public async Task Download_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);
        var controller = CreateController();

        var result = await controller.Download("test.pdf");

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Utilisateur non connecté.", unauthorized.Value);
    }

    [Fact]
    public async Task Download_ReturnsFileStreamResult_WhenFileExists()
    {
        _currentUserService.Setup(s => s.userId).Returns(2);
        var fileStreamResult = new FileStreamResult(new MemoryStream(), "application/pdf");
        _fileService.Setup(s => s.DownloadFileAsync("test.pdf", 2)).ReturnsAsync(fileStreamResult);

        var controller = CreateController();
        var result = await controller.Download("test.pdf");

        Assert.IsType<FileStreamResult>(result);
    }

    [Fact]
    public async Task Download_ReturnsNotFound_WhenFileNotFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(2);
        _fileService.Setup(s => s.DownloadFileAsync("test.pdf", 2)).ThrowsAsync(new FileNotFoundException());

        var controller = CreateController();
        var result = await controller.Download("test.pdf");

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Fichier introuvable.", notFound.Value);
    }

    [Fact]
    public async Task Download_ReturnsServerError_OnException()
    {
        _currentUserService.Setup(s => s.userId).Returns(2);
        _fileService.Setup(s => s.DownloadFileAsync("test.pdf", 2)).ThrowsAsync(new System.Exception("fail"));

        var controller = CreateController();
        var result = await controller.Download("test.pdf");

        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, error.StatusCode);
        Assert.Equal("Une erreur est survenue lors du téléchargement.", error.Value);
    }
}
