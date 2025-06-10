using LodgerBackend.App.Payment.Models;

namespace LodgerBackend.App.Payment.Services;

public interface IPaymentService
{
    Task<List<PaymentDto>> GetPaymentsByUserId(int userId);
}