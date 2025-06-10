using LodgerBackend.Auth.Models.Payloads;
using LodgerBackend.User.Models.Dtos;

namespace LodgerBackend.Auth.Services.Interfaces;

public interface IPasswordResetService
{
    Task<(bool Success, string Message)> RequestReset(RequestPasswordResetPayload payload);
    Task<(bool Success, string Message)> PerformResetAsync(PerformPasswordResetPayload payload);
    Task<(bool Success, string Message)> ChangePassword(int userId, ChangePasswordDto payload);
    Task<bool> ValidateResetToken(string token);
}