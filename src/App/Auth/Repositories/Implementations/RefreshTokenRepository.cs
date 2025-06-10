using LodgerBackend.Auth.Models.Entities;
using LodgerBackend.Auth.Repositories.Interfaces;
using LodgerBackend.Configuration.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.Auth.Repositories.Implementations;

public class RefreshTokenRepository(LodgerDbContext context) : IRefreshTokenRepository
{
    public async Task Save(RefreshToken refreshToken)
    {
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();
    }

    public async Task Delete(RefreshToken existingRefreshToken)
    {
        context.RefreshTokens.Remove(existingRefreshToken);
        await context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenByUserId(int userId)
    {
        return await context.RefreshTokens
            .Where(refreshToken => refreshToken.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task Update(RefreshToken existingToken)
    {
        context.RefreshTokens.Update(existingToken);
        await context.SaveChangesAsync();
    }
}