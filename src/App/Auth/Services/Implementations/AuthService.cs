using System.Net;
using LodgerBackend.App.Auth.Models.Entities;
using LodgerBackend.App.Auth.Models.Payloads;
using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.Device.Models;
using LodgerBackend.App.Device.Services;
using LodgerBackend.App.RentalFile.Services;
using LodgerBackend.App.User.Services;

namespace LodgerBackend.App.Auth.Services.Implementations;

public class AuthService(
    ITokenService tokenService,
    IUserService userService,
    IRefreshTokenService refreshTokenService,
    IDeviceService deviceService,
    IRentalFileService rentalFileService,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<AuthResponse?> HandleLogin(LoginRequest loginRequest)
    {
        logger.LogInformation("Tentative de connexion pour l'identifiant : {Login}", loginRequest.Email);

         if (!await IsValidCredentials(loginRequest.Email, loginRequest.Password))
        {
            logger.LogWarning("Connexion échouée pour : {Login}", loginRequest.Email);
            return null;
        }

        var webUser = await userService.GetByEmail(loginRequest.Email);
        if (webUser == null)
        {
            logger.LogWarning("Utilisateur non trouvé après validation des identifiants : {Login}", loginRequest.Email);
            return null;
        }

        logger.LogInformation("Utilisateur authentifié : {WebUserId}", webUser.Id);
        var refreshToken = await refreshTokenService.GenerateRefreshToken(webUser.Id);

        var deviceConnected = await deviceService.GetDeviceWithIpAddress(webUser.Id);

        // Si aucun refresh token n'est généré ou si le token est null, retourner null
        return refreshToken?.Token != null ? new AuthResponse { Token = refreshToken.Token } : null;
    }

    public async Task ResetPassword(ResetPasswordRequest storedResetRequest,
        PerformPasswordResetPayload performPasswordResetPayload)
    {
        logger.LogInformation("Réinitialisation du mot de passe pour : {WebUserId}", storedResetRequest.UserId);

        var webUser = await userService.GetById(storedResetRequest.UserId);
        if (webUser == null)
        {
            logger.LogWarning("Utilisateur introuvable pour la réinitialisation du mot de passe : {WebUserId}",
                storedResetRequest.UserId);
            return;
        }

        UpdatePassword(webUser, performPasswordResetPayload.NewPassword);
        await tokenService.DeleteResetPasswordToken(storedResetRequest);

        logger.LogInformation("Mot de passe réinitialisé avec succès pour : {WebUserId}", webUser.Id);
    }

    public async Task<(bool success, string message)> HandleSignUp(SignupRequest signupRequest)
    {
        logger.LogInformation("Tentative de création de compte pour : {Email}", signupRequest.Email);

        if (string.IsNullOrWhiteSpace(signupRequest.Email) || string.IsNullOrWhiteSpace(signupRequest.Password))
        {
            logger.LogWarning("Email ou mot de passe vide lors de l'inscription.");
            return (false, "Email et mot de passe requis.");
        }

        if (signupRequest.Password.Length < 8)
        {
            logger.LogWarning("Mot de passe trop court lors de l'inscription pour : {Email}", signupRequest.Email);
            return (false, "Le mot de passe doit contenir au moins 8 caractères.");
        }

        var existingUser = await userService.GetByEmail(signupRequest.Email);
        if (existingUser != null)
        {
            logger.LogWarning("Inscription échouée : un compte existe déjà pour l'email {Email}", signupRequest.Email);
            return (false, "Un compte existe déjà pour cet email.");
        }

        var newUser = new User.Models.Entities.User
        {
            Name = signupRequest.Name,
            Email = signupRequest.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password, 13)
        };
        
        logger.LogInformation("Sauvegarde du nouvel utilisateur : {Email}", newUser.Email);
        var rentalfile = await rentalFileService.AddRentalFile(new RentalFile.Models.RentalFile());
        newUser.RentalFileId = rentalfile.Id;
        await userService.Save(newUser);


        logger.LogInformation("Utilisateur créé avec succès : {Email}", newUser.Email);

        var refreshToken = await refreshTokenService.GenerateRefreshToken(newUser.Id);
        if (refreshToken == null)
        {
            logger.LogWarning("Échec de la génération du refresh token à l'inscription pour : {Email}", newUser.Email);
            return (true, "Compte créé, mais échec lors de la génération du jeton.");
        }

        logger.LogInformation("Jeton de rafraîchissement généré avec succès pour : {WebUserId}", newUser.Id);
        
        return (true, "Compte créé avec succès.");
    }

    private async Task<bool> IsValidCredentials(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            logger.LogWarning("Email ou mot de passe vide lors d'une tentative de connexion.");
            return false;
        }

        if (email.Length < 4 || password.Length < 8)
        {
            logger.LogWarning("Email ou mot de passe trop court : email({LengthEmail}) / password({LengthPassword})", email.Length, password.Length);
            return false;
        }

        var webUser = await userService.GetByEmail(email);
        if (webUser == null)
        {
            logger.LogWarning("Aucun utilisateur trouvé avec l'identifiant : {Login}", email);
            return false;
        }

        var isValid = BCrypt.Net.BCrypt.Verify(password, webUser.Password);
        if (isValid)
        {
            logger.LogInformation("Connexion réussie pour : {WebUserId}", webUser.Id);
        }
        else
        {
            logger.LogWarning("Mot de passe incorrect pour l'utilisateur : {WebUserId}", webUser.Id);
        }

        return isValid;
    }
    public async Task<(bool success, string message)> ChangePassword(int userId, string newPassword, string oldPassword)
    {
        logger.LogInformation("Réinitialisation du mot de passe pour : {WebUserId}", userId);

        var webUser = await userService.GetById(userId);
        if (webUser == null)
        {
            logger.LogWarning("Utilisateur introuvable pour la réinitialisation du mot de passe : {WebUserId}",
                userId);
            return (false, "Utilisateur non trouvé.");
        }
        var isValid = await IsValidCredentials(webUser.Email, oldPassword);
        if (!isValid)
        {
            logger.LogWarning("Connexion échouée pour : {Login}", webUser.Email);
            return (false, $"Mot de passe incorrect pour l'utilisateur {webUser.Email}.");
        }

        isValid = UpdatePassword(webUser, newPassword);
        if (!isValid) return (false, "Le nouveau mot de passe ne respecte pas les normes.");

        await userService.Save(webUser);

        logger.LogInformation("Mot de passe réinitialisé avec succès pour : {WebUserId}", webUser.Id);
        return (true, "Mot de passe réinitialisé");
    }

    private bool UpdatePassword(User.Models.Entities.User user, string newPassword)
    {
        logger.LogInformation("Mise à jour du mot de passe pour : {WebUserId}", user.Id);
        if (string.IsNullOrWhiteSpace(newPassword))
        {
            logger.LogWarning("Mot de passe vide.");
            return false;
        }

        if (newPassword.Length < 8)
        {
            logger.LogWarning("Mot de passe trop court : password({LengthPassword})", newPassword.Length);
            return false;
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, 13);
        logger.LogInformation("Mot de passe mis à jour avec succès pour : {WebUserId}", user.Id);      
        
        return true;
    }
}