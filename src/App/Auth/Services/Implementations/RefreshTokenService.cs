using LodgerBackend.App.Auth.Models.Entities;
using LodgerBackend.App.Auth.Models.Payloads;
using LodgerBackend.App.Auth.Repositories.Interfaces;
using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.User.Repositories;

namespace LodgerBackend.App.Auth.Services.Implementations;

public class RefreshTokenService(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository webUsersRepository,
    ITokenService tokenService,
    ILogger<RefreshTokenService> logger) : IRefreshTokenService
{
    private const int RefreshTokenValidityDays = 7;
    private const int AccessTokenValidityHours = 5;

    public async Task<RefreshToken?> GenerateRefreshToken(int userId)
    {
        logger.LogInformation("Génération du refresh token pour l'utilisateur avec ID {UserId}", userId);

        var webUser = await webUsersRepository.GetById(userId);
        if (webUser is null)
        {
            logger.LogWarning("Utilisateur avec ID {UserId} non trouvé.", userId);
            return null;
        }

        var existingToken = await refreshTokenRepository.GetRefreshTokenByUserId(userId);

        // Cas 1 : un token existe mais il est expiré ou révoqué → on le supprime
        if (existingToken is not null && (existingToken.IsRevoked || existingToken.Expiration <= DateTime.UtcNow))
        {
            logger.LogWarning("Refresh token expiré ou révoqué, suppression du token existant");
            await refreshTokenRepository.Delete(existingToken);
            existingToken = null;
        }

        // Cas 2 : un token valide existe, mais l'access token est encore valable
        if (existingToken is not null && existingToken.AccesTokenExpiration > DateTime.UtcNow)
        {
            logger.LogInformation("Access token encore valide, retour du token existant");
            return existingToken;
        }

        // Cas 3 : token existant mais access token expiré → on met à jour
        if (existingToken is not null)
        {
            logger.LogInformation("Access token expiré, mise à jour du token existant");
            existingToken.Token = tokenService.GenerateAuthToken(webUser);
            existingToken.AccesTokenExpiration = DateTime.UtcNow.AddHours(AccessTokenValidityHours);
            existingToken.Expiration = DateTime.UtcNow.AddDays(RefreshTokenValidityDays);
            await refreshTokenRepository.Update(existingToken);
            
            return existingToken;
        }

        // Cas 4 : aucun token existant → on en crée un nouveau
        logger.LogInformation("Aucun refresh token valide, création d'un nouveau");
        var newToken = new RefreshToken
        {
            UserId = webUser.Id,
            Token = tokenService.GenerateAuthToken(webUser),
            IsRevoked = false,
            Expiration = DateTime.UtcNow.AddDays(RefreshTokenValidityDays),
            AccesTokenExpiration = DateTime.UtcNow.AddHours(AccessTokenValidityHours)
        };

        await refreshTokenRepository.Save(newToken);
        return newToken;
    }

    public async Task<AuthResponse?> RefreshAccessTokenByWebUserId(int userId)
    {
        var refreshToken = await GenerateRefreshToken(userId);

        return refreshToken?.Token is not null
            ? new AuthResponse { Token = refreshToken.Token }
            : null;
    }
}