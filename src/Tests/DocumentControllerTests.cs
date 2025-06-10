using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Document.Services;
using LodgerBackend.Document;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests;

public class DocumentControllerTests
{
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<IDocumentService> _documentService = new();

    private DocumentController CreateController()
    {
        return new DocumentController(_currentUserService.Object, _documentService.Object);
    }

    [Fact]
    public async Task GetDocuments_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);

        var controller = CreateController();
        var result = await controller.GetDocuments();

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Utilisateur non connecté.", unauthorized.Value);
    }

    [Fact]
    public async Task GetDocuments_ReturnsOk_WhenDocumentsFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(7);
        var documents = new List<LodgerBackend.Document.Models.DocumentDto>
        {
            new() { FileName = "test.pdf", DocumentCategorie = "cat1", DocumentType = "type1" }
        };
        _documentService.Setup(s => s.GetAllByUserId(7)).ReturnsAsync(documents);

        var controller = CreateController();
        var result = await controller.GetDocuments();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(documents, okResult.Value);
    }

    [Fact]
    public async Task GetDocuments_ReturnsServerError_OnException()
    {
        _currentUserService.Setup(s => s.userId).Returns(7);
        _documentService.Setup(s => s.GetAllByUserId(7)).ThrowsAsync(new System.Exception("fail"));

        var controller = CreateController();
        var result = await controller.GetDocuments();

        var errorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, errorResult.StatusCode);
        Assert.Equal("Une erreur est survenue lors de la récupération des documents.", errorResult.Value);
    }
}
