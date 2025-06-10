using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.Payment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.App.Payment;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(
    ICurrentUserService currentUserService,
    IPaymentService paymentService) : ControllerBase
{
    /// <summary>
    /// Récupère les paiements de l'utilisateur connecté.
    /// </summary>
    /// <returns>Liste des paiements.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPayments()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized("Utilisateur non connecté.");

        try
        {
            var payments = await paymentService.GetPaymentsByUserId(userId.Value);
            return Ok(payments);
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Une erreur est survenue lors de la récupération des paiements.");
        }
    }
}