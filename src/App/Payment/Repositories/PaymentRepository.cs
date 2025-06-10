using LodgerBackend.Configuration.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.Payment.Repositories;

public class PaymentRepository(LodgerDbContext dbContext) : IPaymentRepository
{
    public async Task<List<Models.Payment>> GetPaymentsByUserId(int userId)
    {
        return await dbContext.Payments
            .Where(payment => payment.UserId == userId)
            .ToListAsync();
    }
}