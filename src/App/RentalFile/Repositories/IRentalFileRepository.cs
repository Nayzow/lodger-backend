namespace LodgerBackend.App.RentalFile.Repositories;

public interface IRentalFileRepository
{
    Task<Models.RentalFile> AddRentalFile(Models.RentalFile rentalFile);
    Task<Models.RentalFile?> GetRentalFileByUserId(int userId);
    Task<Models.RentalFile> Save(Models.RentalFile updateRentalFile);
}