using LodgerBackend.App.Auth.Models.Entities;
using LodgerBackend.App.Auth.Models.Payloads;

namespace LodgerBackend.App.Auth.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken?> GenerateRefreshToken(int userId);
    Task<AuthResponse?> RefreshAccessTokenByWebUserId(int userId);
}