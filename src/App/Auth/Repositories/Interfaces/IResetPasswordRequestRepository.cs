using LodgerBackend.App.Auth.Models.Entities;

namespace LodgerBackend.App.Auth.Repositories.Interfaces;

public interface IResetPasswordRequestRepository
{
    Task<ResetPasswordRequest?> FindById(int id);
    Task<ResetPasswordRequest?> FindByToken(string token);
    Task Save(ResetPasswordRequest resetPasswordRequest);
    Task Delete(ResetPasswordRequest resetPasswordRequest);
    Task Update(ResetPasswordRequest resetPasswordRequest);
}