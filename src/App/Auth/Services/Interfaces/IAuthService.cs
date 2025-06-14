using LodgerBackend.Auth.Models.Entities;
using LodgerBackend.Auth.Models.Payloads;

namespace LodgerBackend.Auth.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> HandleLogin(LoginRequest loginRequest);
    Task ResetPassword(ResetPasswordRequest storedResetRequest, PerformPasswordResetPayload performPasswordResetPayload);
    Task<(bool success, string message)> ChangePassword(int userId, string newPassword, string oldPassword);
    Task<(bool success, string message)> HandleSignUp(SignupRequest signupRequest);
}