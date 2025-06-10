using LodgerBackend.App.User.Models.Dtos;

namespace LodgerBackend.App.User.Services;

public interface IUserService
{
    Task<UserDetailsDto> GetUserWithDetailsAsync(int userId);
    Task<Models.Entities.User?> GetByEmail(string email);
    Task<Models.Entities.User?> GetById(int userId);
    Task Save(Models.Entities.User newUser);
    Task<UserDetailsDto> UpdateUserWithDetailsAsync(UserDetailsDto userDetailsDto, Models.Entities.User user);
    Task DeleteUserByIdAsync(int userId);
}
