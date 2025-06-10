namespace LodgerBackend.Auth.Services.Interfaces;

public interface ICurrentUserService
{
    int? userId { get; }
    List<string> Roles { get; }
    Task<User.Models.Entities.User?> GetUser();
}