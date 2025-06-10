namespace LodgerBackend.Auth.Services.Interfaces;

public interface IAuthMailsService
{
    Task SendResetPasswordMail(User.Models.Entities.User user, string token);
}
