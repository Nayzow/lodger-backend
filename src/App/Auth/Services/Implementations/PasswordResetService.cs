using LodgerBackend.App.Auth.Models.Payloads;
using LodgerBackend.App.Auth.Repositories.Interfaces;
using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.User.Models.Dtos;
using LodgerBackend.App.User.Services;

namespace LodgerBackend.App.Auth.Services.Implementations;

public class PasswordResetService(
    IResetPasswordRequestRepository resetPasswordRequestRepository,
    ITokenService tokenService,
    IAuthService authService,
    IUserService webUsersService,
    IAuthMailsService mailsService,
    ILogger<PasswordResetService> logger) : IPasswordResetService
{
    public async Task<(bool Success, string Message)> RequestReset(RequestPasswordResetPayload payload)
    {
        logger.LogInformation("Demande de réinitialisation pour : {Email}", payload.Email);
        var user = await webUsersService.GetByEmail(payload.Email);
        
        if (user == null)
        {
            logger.LogWarning("Aucun utilisateur trouvé.");
            return (false, "Utilisateur introuvable.");
        }

        var token = await tokenService.GenerateResetPasswordToken(user);

        try
        {
            await mailsService.SendResetPasswordMail(user, token);
            logger.LogInformation("Mail de réinitialisation envoyé.");
            
            return (true, "E-mail envoyé.");
        }
        
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'envoi du mail.");
            return (false, "Erreur lors de l'envoi.");
        }
    }

    public async Task<(bool Success, string Message)> PerformResetAsync(PerformPasswordResetPayload payload)
    {
        logger.LogInformation("Réinitialisation du mot de passe...");

        if (payload.NewPassword != payload.ConfirmPassword)
        {
            logger.LogWarning("Mots de passe non concordants.");
            return (false, "Les mots de passe ne correspondent pas.");
        }

        if (string.IsNullOrWhiteSpace(payload.ResetPasswordRequest.Token))
        {
            logger.LogWarning("Token absent.");
            return (false, "Token invalide.");
        }

        var storedRequest = await resetPasswordRequestRepository.FindByToken(payload.ResetPasswordRequest.Token);
        if (storedRequest == null)
        {
            logger.LogWarning("Token non reconnu.");
            return (false, "Token expiré ou invalide.");
        }

        await authService.ResetPassword(storedRequest, payload);
        logger.LogInformation("Mot de passe réinitialisé avec succès.");
        
        return (true, "Mot de passe mis à jour.");
    }

    public async Task<bool> ValidateResetToken(string token)
    {
        logger.LogInformation("Validation du token : {Token}", token);
        return await tokenService.ValidateResetPasswordToken(token);
    }

    public async Task<(bool Success, string Message)> ChangePassword(int userId, ChangePasswordDto payload)
    {
        logger.LogInformation("Réinitialisation du mot de passe...");

        if (payload.NewPassword != payload.ConfirmNewPassword)
        {
            logger.LogWarning("Mots de passe non concordants.");
            return (false, "Les mots de passe ne correspondent pas.");
        }

        var (success, message) = await authService.ChangePassword(userId, payload.NewPassword, payload.Password);
        logger.LogInformation("Mot de passe réinitialisé avec succès.");

        return (success, message);
    }
}