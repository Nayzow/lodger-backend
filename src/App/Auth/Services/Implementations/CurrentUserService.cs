using System.Security.Claims;
using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.User.Repositories;

namespace LodgerBackend.App.Auth.Services.Implementations;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository webUsersRepository) : ICurrentUserService
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    public int? userId => GetGuidClaim("webUserId");

    public List<string> Roles => _user?.FindAll(ClaimTypes.Role).Select(role => role.Value).ToList() ?? [];

    public async Task<User.Models.Entities.User?> GetUser()
    {
        if (userId == null) return null;
        return await webUsersRepository.GetById(userId.Value);
    }
    
    private int? GetGuidClaim(string claimType)
    {
        var value = _user?.FindFirst(claimType)?.Value;
        return int.TryParse(value, out var id) ? id : null;
    }
}