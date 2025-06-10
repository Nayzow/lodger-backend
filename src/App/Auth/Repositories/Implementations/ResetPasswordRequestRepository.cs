using LodgerBackend.App.Auth.Models.Entities;
using LodgerBackend.App.Auth.Repositories.Interfaces;
using LodgerBackend.App.Configuration.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.Auth.Repositories.Implementations;
public class ResetPasswordRequestRepository(LodgerDbContext dbContext) : IResetPasswordRequestRepository
{
    public async Task<ResetPasswordRequest?> FindById(int id)
    {
        return await dbContext.ResetPasswordRequests.FindAsync(id);
    }
    
    public async Task<ResetPasswordRequest?> FindByToken(string token)
    {
        return await dbContext.ResetPasswordRequests.FirstOrDefaultAsync(r => r.Token == token);
    }

    public async Task Save(ResetPasswordRequest resetRequest)
    {
        dbContext.ResetPasswordRequests.Add(resetRequest);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(ResetPasswordRequest resetRequest)
    {
        dbContext.Entry(resetRequest).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(ResetPasswordRequest resetRequest)
    {
        dbContext.Entry(resetRequest).State = EntityState.Detached;
        dbContext.Attach(resetRequest);
        dbContext.ResetPasswordRequests.Remove(resetRequest);
        await dbContext.SaveChangesAsync();
    }
}