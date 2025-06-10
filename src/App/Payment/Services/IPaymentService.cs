using LodgerBackend.Payment.Models;

namespace LodgerBackend.Payment.Services;

public interface IPaymentService
{
    Task<List<PaymentDto>> GetPaymentsByUserId(int userId);
}