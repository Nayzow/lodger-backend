using LodgerBackend.Configuration.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.User.Repositories;

public class UserRepository(LodgerDbContext dbContext) : IUserRepository
{
    public async Task<Models.Entities.User?> GetUserWithDetailsAsync(int userId)
    {
        return await dbContext.Users
            .Include(user => user.UserRoles)
            .ThenInclude(userRole => userRole.Role)
            .Include(user => user.Address)
            .FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<Models.Entities.User?> GetByEmail(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<Models.Entities.User?> GetById(int userId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<User.Models.Entities.User> Save(User.Models.Entities.User user)
    {
        if (user.Id == 0)
        {
            dbContext.Users.Add(user);
        }
        else
        {
            dbContext.Users.Update(user);
        }

        await dbContext.SaveChangesAsync();
        return user;
    }
    public async Task DeleteUserWithDependenciesAsync(int userId)
    {
        var user = await dbContext.Users
            .Include(u => u.Comments)
            .Include(u => u.Devices)
            .Include(u => u.Documents)
            .Include(u => u.Payments)
            .Include(u => u.RentalFile)
            .Include(u => u.Settings)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            throw new Exception($"Utilisateur avec l'ID {userId} introuvable.");

        dbContext.Comments.RemoveRange(user.Comments);
        dbContext.Devices.RemoveRange(user.Devices);
        dbContext.Documents.RemoveRange(user.Documents);
        dbContext.Payments.RemoveRange(user.Payments);
        if(user.RentalFile is not null) dbContext.RentalFiles.RemoveRange(user.RentalFile);
        if(user.Settings is not null) dbContext.Settings.RemoveRange(user.Settings);


        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync();
    }


}