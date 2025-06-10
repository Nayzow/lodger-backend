using LodgerBackend.App.Auth.Models.Payloads;
using LodgerBackend.App.User.Models.Dtos;

namespace LodgerBackend.App.Auth.Services.Interfaces;

public interface IPasswordResetService
{
    Task<(bool Success, string Message)> RequestReset(RequestPasswordResetPayload payload);
    Task<(bool Success, string Message)> PerformResetAsync(PerformPasswordResetPayload payload);
    Task<(bool Success, string Message)> ChangePassword(int userId, ChangePasswordDto payload);
    Task<bool> ValidateResetToken(string token);
}