using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Payment.Services;
using LodgerBackend.Payment;
using System.Collections.Generic;
using System.Threading.Tasks;
using LodgerBackend.Payment.Models;

namespace Tests;

public class PaymentControllerTests
{
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<IPaymentService> _paymentService = new();

    private PaymentController CreateController()
    {
        return new PaymentController(_currentUserService.Object, _paymentService.Object);
    }

    [Fact]
    public async Task GetPayments_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);

        var controller = CreateController();
        var result = await controller.GetPayments();

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Utilisateur non connecté.", unauthorized.Value);
    }

    [Fact]
    public async Task GetPayments_ReturnsOk_WhenPaymentsFound()
    {
        _currentUserService.Setup(s => s.userId).Returns(5);
        var payments = new List<PaymentDto>
        {
            new() { Id = 1, UserId = 5, Amount = 100.0m }
        };
        _paymentService.Setup(s => s.GetPaymentsByUserId(5)).ReturnsAsync(payments);

        var controller = CreateController();
        var result = await controller.GetPayments();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(payments, okResult.Value);
    }

    [Fact]
    public async Task GetPayments_ReturnsServerError_OnException()
    {
        _currentUserService.Setup(s => s.userId).Returns(5);
        _paymentService.Setup(s => s.GetPaymentsByUserId(5)).ThrowsAsync(new System.Exception("fail"));

        var controller = CreateController();
        var result = await controller.GetPayments();

        var errorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, errorResult.StatusCode);
        Assert.Equal("Une erreur est survenue lors de la récupération des paiements.", errorResult.Value);
    }
}
