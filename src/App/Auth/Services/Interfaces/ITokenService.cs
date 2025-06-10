using LodgerBackend.App.Auth.Models.Entities;

namespace LodgerBackend.App.Auth.Services.Interfaces;

public interface ITokenService
{
    string GenerateAuthToken(User.Models.Entities.User webUser);
    Task<string> GenerateResetPasswordToken(User.Models.Entities.User user);
    Task<bool> ValidateResetPasswordToken(string token);
    Task DeleteResetPasswordToken(ResetPasswordRequest resetPasswordRequest);
}