using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LodgerBackend.Auth.Models.Entities;
using LodgerBackend.Auth.Repositories.Interfaces;
using LodgerBackend.Auth.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LodgerBackend.Auth.Services.Implementations;

public class TokenService(
    IConfiguration configuration,
    IResetPasswordRequestRepository resetPasswordRequestRepository,
    ILogger<TokenService> logger) : ITokenService
{
    private readonly byte[] _key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"] ?? string.Empty);

    public string GenerateAuthToken(User.Models.Entities.User webUser)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, webUser.Id.ToString()),
                new("webUserId", webUser.Id.ToString())
            };

            // var userRoles = webUser.UserRoles
            //     .Select(wur => wur.WebRole.Nom)
            //     .Distinct();

            // claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            logger.LogInformation("Token JWT généré pour l'utilisateur {UserId}", webUser.Id);

            return jwt;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la génération du token JWT pour l'utilisateur {UserId}", webUser.Id);
            throw;
        }
    }

    public async Task<string> GenerateResetPasswordToken(User.Models.Entities.User user)
    {
        try
        {
            var token = GenerateRandomToken();

            var resetRequest = new ResetPasswordRequest
            {
                UserId = user.Id,
                Token = token,
                RequestedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };

            await resetPasswordRequestRepository.Save(resetRequest);

            logger.LogInformation("Token de réinitialisation généré pour l'utilisateur {UserId}", user.Id);

            return token;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la génération du token de réinitialisation pour l'utilisateur {UserId}", user.Id);
            throw;
        }
    }

    public async Task<bool> ValidateResetPasswordToken(string token)
    {
        try
        {
            var resetRequest = await resetPasswordRequestRepository.FindByToken(token);

            var isValid = resetRequest != null &&
                          resetRequest.RequestedAt < DateTime.UtcNow &&
                          DateTime.UtcNow < resetRequest.ExpiresAt;

            logger.LogInformation("Validation du token de réinitialisation : {Token} -> {IsValid}", token, isValid);
            return isValid;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la validation du token de réinitialisation : {Token}", token);
            throw;
        }
    }

    public async Task DeleteResetPasswordToken(ResetPasswordRequest resetPasswordRequest)
    {
        try
        {
            await resetPasswordRequestRepository.Delete(resetPasswordRequest);
            logger.LogInformation("Token de réinitialisation supprimé pour l'utilisateur {UserId}", resetPasswordRequest.UserId);
        }
            
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la suppression du token de réinitialisation pour l'utilisateur {UserId}", resetPasswordRequest.UserId);
            throw;
        }
    }

    private static string GenerateRandomToken()
    {
        const int tokenLength = 32;
        var tokenBytes = new byte[tokenLength];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(tokenBytes);

        return BitConverter.ToString(tokenBytes).Replace("-", "").ToLower();
    }
}
