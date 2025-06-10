namespace LodgerBackend.Payment.Repositories;

public interface IPaymentRepository
{
    Task<List<Models.Payment>> GetPaymentsByUserId(int userId);
}