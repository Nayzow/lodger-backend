using LodgerBackend.Configuration.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Task = System.Threading.Tasks.Task;

namespace LodgerBackend.Mail;

public abstract class MailsService(IOptions<SmtpSettings> smtpSettings, ILogger<MailsService> logger)
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;
    private readonly ILogger _logger = logger;
    private SmtpClient? _smtpClient;
    private bool _isConnected;

    protected async Task SendMail(string toEmail, string subject, string body)
    {
        try
        {
            if (!_isConnected)
            {
                _smtpClient = new SmtpClient();
                _logger.LogInformation("Connexion au serveur SMTP {Host}:{Port}", _smtpSettings.Host, _smtpSettings.Port);
                await _smtpClient.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port);
                _smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                _isConnected = true;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            message.To.Add(new MailboxAddress(toEmail, toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = GetHtmlTemplate(body),
                TextBody = body
            };

            message.Body = bodyBuilder.ToMessageBody();

            if (_smtpClient != null)
            {
                _logger.LogInformation("Envoi du mail à {ToEmail} avec le sujet : {Subject}", toEmail, subject);
                await _smtpClient.SendAsync(message);
                _logger.LogInformation("Mail envoyé avec succès à {ToEmail}", toEmail);
            }
        }
        catch (Exception ex)
        {
            _isConnected = false;
            _logger.LogError(ex, "Erreur lors de l'envoi du mail à {ToEmail}", toEmail);
            throw new Exception("Erreur lors de l'envoi du mail", ex);
        }
    }

    private static string GetHtmlTemplate(string body)
    {
        return $"""
                   <html>
                       <head>
                            <style>
                            </style>
                       </head>
                       <body>
                           <p>Bonjour,</p>
                           <p>{body}</p>
                           <p>Cordialement,</p>
                           <p><strong>L'équipe Lodger</strong></p>
                       </body>
                   </html>
                """;
    }
}