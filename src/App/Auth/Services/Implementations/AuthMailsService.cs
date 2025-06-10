using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Configuration.Settings;
using LodgerBackend.Mail;
using Microsoft.Extensions.Options;

namespace LodgerBackend.Auth.Services.Implementations;

public class AuthMailsService(IOptions<SmtpSettings> smtpSettings, IConfiguration configuration, ILogger<MailsService> logger) : MailsService(smtpSettings, logger), IAuthMailsService
{
    public async Task SendResetPasswordMail(User.Models.Entities.User user, string resetToken)
    {
        var resetLink = $"{configuration["ServerSettings:ServerUrl"]}/reset-password?token={resetToken}";

        var body = $"""
                    <p>Vous avez demandé une réinitialisation de mot de passe. 
                    Veuillez cliquer sur le bouton ci-dessous pour réinitialiser votre mot de passe :</p>

                    <p>
                        <a href="{resetLink}" target="_blank" class='btn'>
                            Réinitialiser mon mot de passe
                        </a>
                    </p>

                    <p>Si vous n'avez pas demandé cette réinitialisation, veuillez ignorer ce message.</p>
                    """;

        await SendMail(user.Email, "Demande de réinitialisation de mot de passe", body);
    }
}