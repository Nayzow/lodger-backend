using LodgerBackend.RentalFile.Models;

namespace LodgerBackend.RentalFile.Services;

public interface IRentalFileService
{
    Task<Models.RentalFile> AddRentalFile(Models.RentalFile rentalFile);
    Task<Models.RentalFile?> GetRentalFileByUserId(int userId);
    Task<RentalFileDto?> GetRentalFileDetailsByUserId(int userId);
    Task<RentalFileDto?> UpdateRentalFileAsync(int userId, UpdateRentalFileDto rentalFileDto, Models.RentalFile rentalFile);
}