using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LodgerBackend.Auth;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Auth.Models.Payloads;
using System.Threading.Tasks;
using LodgerBackend.Auth.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper.Internal;

namespace Tests;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _authService = new();
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<IRefreshTokenService> _refreshTokenService = new();
    private readonly Mock<IPasswordResetService> _passwordResetService = new();

    private AuthController CreateController()
    {
        var controller = new AuthController(
            _currentUserService.Object,
            _authService.Object,
            _refreshTokenService.Object,
            _passwordResetService.Object
        );
        controller.ModelState.Clear();
        return controller;
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenCredentialsAreValid()
    {
        var loginRequest = new LoginRequest { Email = "test@test.fr", Password = "Password123!" };
        var authResponse = new AuthResponse { Token = "token" };
        _authService.Setup(s => s.HandleLogin(loginRequest)).ReturnsAsync(authResponse);

        var controller = CreateController();
        var result = await controller.Login(loginRequest);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(authResponse, okResult.Value);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        var loginRequest = new LoginRequest { Email = "test@test.fr", Password = "wrong" };
        _authService.Setup(s => s.HandleLogin(loginRequest)).ReturnsAsync((AuthResponse?)null);

        var controller = CreateController();
        var result = await controller.Login(loginRequest);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        var loginRequest = new LoginRequest { Email = "", Password = "" };
        var controller = CreateController();
        controller.ModelState.AddModelError("Email", "Required");

        var result = await controller.Login(loginRequest);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Refresh_ReturnsUnauthorized_WhenUserIdIsNull()
    {
        _currentUserService.Setup(s => s.userId).Returns((int?)null);

        var controller = CreateController();
        var result = await controller.Refresh();

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.NotNull(unauthorized.Value); // Ensure Value is not null
        Assert.Contains("User ID not found", unauthorized.Value.ToString());
    }

    [Fact]
    public async Task Refresh_ReturnsOk_WhenTokenIsRefreshed()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        var authResponse = new AuthResponse { Token = "newtoken" };
        _refreshTokenService.Setup(s => s.RefreshAccessTokenByWebUserId(1)).ReturnsAsync(authResponse);

        var controller = CreateController();
        var result = await controller.Refresh();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(authResponse, okResult.Value);
    }

    [Fact]
    public async Task Refresh_ReturnsUnauthorized_WhenUnauthorizedAccessException()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        _refreshTokenService.Setup(s => s.RefreshAccessTokenByWebUserId(1)).ThrowsAsync(new UnauthorizedAccessException("expired"));

        var controller = CreateController();
        var result = await controller.Refresh();

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.NotNull(unauthorized.Value); // Ensure Value is not null
        Assert.Contains("expired", unauthorized.Value.ToString());
    }

    [Fact]
    public async Task Refresh_ReturnsServerError_WhenException()
    {
        _currentUserService.Setup(s => s.userId).Returns(1);
        _refreshTokenService.Setup(s => s.RefreshAccessTokenByWebUserId(1)).ThrowsAsync(new System.Exception());

        var controller = CreateController();
        var result = await controller.Refresh();

        var serverError = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, serverError.StatusCode);
    }

    [Fact]
    public async Task SignUp_ReturnsOk_WhenSuccess()
    {
        var signupRequest = new SignupRequest { Name = "Test", Email = "test@test.fr", Password = "Password123" };
        _authService.Setup(s => s.HandleSignUp(signupRequest)).ReturnsAsync((true, "ok"));

        var controller = CreateController();
        var result = await controller.SignUp(signupRequest);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.Contains("ok", okResult.Value.ToString());
    }

    [Fact]
    public async Task SignUp_ReturnsUnauthorized_WhenFailed()
    {
        var signupRequest = new SignupRequest { Name = "Test", Email = "test@test.fr", Password = "Password123" };
        _authService.Setup(s => s.HandleSignUp(signupRequest)).ReturnsAsync((false, "error"));

        var controller = CreateController();
        var result = await controller.SignUp(signupRequest);

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("error", unauthorized.Value);
    }

    [Fact]
    public async Task SignUp_ReturnsBadRequest_WhenPasswordTooShort()
    {
        var signupRequest = new SignupRequest { Name = "Test", Email = "test@test.fr", Password = "short" };

        var controller = CreateController();
        var result = await controller.SignUp(signupRequest);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequest.Value);
        Assert.Contains("Le mot de passe doit comporter au moins 8 caractères.", badRequest.Value.ToString());
    }

    [Fact]
    public async Task SignUp_ReturnsBadRequest_WhenModelStateInvalid()
    {
        var signupRequest = new SignupRequest { Name = "", Email = "", Password = "Password123" };
        var controller = CreateController();
        controller.ModelState.AddModelError("Email", "Required");

        var result = await controller.SignUp(signupRequest);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ResetPassword_ReturnsOk_WhenSuccess()
    {
        var payload = new PerformPasswordResetPayload
        {
            NewPassword = "Password123",
            ConfirmPassword = "Password123",
            ResetPasswordRequest = new ResetPasswordRequest()
        };
        _passwordResetService.Setup(s => s.PerformResetAsync(payload)).ReturnsAsync((true, "ok"));

        var controller = CreateController();
        var result = await controller.ResetPassword(payload);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.Contains("ok", okResult.Value.ToString());
    }

    [Fact]
    public async Task ResetPassword_ReturnsBadRequest_WhenFailed()
    {
        var payload = new PerformPasswordResetPayload
        {
            NewPassword = "Password123",
            ConfirmPassword = "Password123",
            ResetPasswordRequest = new ResetPasswordRequest()
        };
        _passwordResetService.Setup(s => s.PerformResetAsync(payload)).ReturnsAsync((false, "error"));

        var controller = CreateController();
        var result = await controller.ResetPassword(payload);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("error", badRequest.Value);
    }

    [Fact]
    public async Task ResetPassword_ReturnsBadRequest_WhenModelStateInvalid()
    {
        var payload = new PerformPasswordResetPayload
        {
            NewPassword = "",
            ConfirmPassword = "",
            ResetPasswordRequest = new ResetPasswordRequest()
        };
        var controller = CreateController();
        controller.ModelState.AddModelError("NewPassword", "Required");

        var result = await controller.ResetPassword(payload);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ResetPasswordRequest_ReturnsOk_WhenSuccess()
    {
        var payload = new RequestPasswordResetPayload { Email = "test@test.fr" };
        _passwordResetService.Setup(s => s.RequestReset(payload)).ReturnsAsync((true, "ok"));

        var controller = CreateController();
        var result = await controller.ResetPasswordRequest(payload);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.Contains("ok", okResult.Value.ToString());
    }

    [Fact]
    public async Task ResetPasswordRequest_ReturnsServerError_WhenFailed()
    {
        var payload = new RequestPasswordResetPayload { Email = "test@test.fr" };
        _passwordResetService.Setup(s => s.RequestReset(payload)).ReturnsAsync((false, "error"));

        var controller = CreateController();
        var result = await controller.ResetPasswordRequest(payload);

        var serverError = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, serverError.StatusCode);
        Assert.Equal("error", serverError.Value);
    }

    [Fact]
    public async Task ResetPasswordRequest_ReturnsBadRequest_WhenModelStateInvalid()
    {
        var payload = new RequestPasswordResetPayload { Email = "" };
        var controller = CreateController();
        controller.ModelState.AddModelError("Email", "Required");

        var result = await controller.ResetPasswordRequest(payload);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
