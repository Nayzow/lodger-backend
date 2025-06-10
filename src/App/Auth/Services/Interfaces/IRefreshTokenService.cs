using LodgerBackend.Auth.Models.Entities;
using LodgerBackend.Auth.Models.Payloads;

namespace LodgerBackend.Auth.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken?> GenerateRefreshToken(int userId);
    Task<AuthResponse?> RefreshAccessTokenByWebUserId(int userId);
}