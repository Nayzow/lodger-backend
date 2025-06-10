using LodgerBackend.Configuration.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.RentalFile.Repositories;

public class RentalFileRepository(LodgerDbContext dbContext) : IRentalFileRepository
{
    public async Task<Models.RentalFile> AddRentalFile(Models.RentalFile rentalFile)
    {
        dbContext.RentalFiles.Add(rentalFile);
        await dbContext.SaveChangesAsync();
        return rentalFile;
    }
    public async Task<Models.RentalFile?> GetRentalFileByUserId(int userId)
    {
        return await dbContext.RentalFiles
            .Include(u => u.User)
            .Where(rentalFile => rentalFile.User != null && rentalFile.User.Id == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<Models.RentalFile> Save(Models.RentalFile updateRentalFile)
    {
        dbContext.RentalFiles.Update(updateRentalFile);
        await dbContext.SaveChangesAsync();
        return updateRentalFile;

    }
}