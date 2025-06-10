namespace LodgerBackend.App.User.Repositories;

public interface IUserRepository
{
    Task<Models.Entities.User?> GetUserWithDetailsAsync(int userId);
    Task<Models.Entities.User?> GetByEmail(string email);
    Task<Models.Entities.User?> GetById(int userId);
    Task<Models.Entities.User> Save(Models.Entities.User newUser);
    Task DeleteUserWithDependenciesAsync(int userId);
}