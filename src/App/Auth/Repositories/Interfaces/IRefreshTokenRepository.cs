using LodgerBackend.App.Auth.Models.Entities;

namespace LodgerBackend.App.Auth.Repositories.Interfaces;

public interface IRefreshTokenRepository
{ 
    Task Save(RefreshToken refreshToken);
    Task Delete(RefreshToken existingRefreshToken);
    Task<RefreshToken?> GetRefreshTokenByUserId(int userId);
    Task Update(RefreshToken existingToken);
}