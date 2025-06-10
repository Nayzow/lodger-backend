using AutoMapper;
using LodgerBackend.Payment.Models;
using LodgerBackend.Payment.Repositories;

namespace LodgerBackend.Payment.Services;

public class PaymentService(IPaymentRepository paymentRepository, ILogger<PaymentService> logger, IMapper mapper) : IPaymentService
{
    public async Task<List<PaymentDto>> GetPaymentsByUserId(int userId)
    {
        try
        {
            var payments = await paymentRepository.GetPaymentsByUserId(userId);

            if (payments.Count == 0)
            {
                logger.LogWarning("Aucun paiement trouvé pour l'utilisateur {UserId}.", userId);
                return [];
            }

            var paymentsDto = payments.Select(payment => mapper.Map<Models.PaymentDto>(payment)).ToList();
            logger.LogInformation("Paiements récupérés pour l'utilisateur {UserId} : {Count} paiement(s).", userId, paymentsDto.Count);

            return paymentsDto;
        }
        
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération des paiements pour l'utilisateur {UserId}.", userId);
            throw;
        }
    }
}