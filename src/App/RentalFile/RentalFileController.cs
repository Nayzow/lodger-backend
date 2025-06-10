using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.RentalFile.Models;
using LodgerBackend.App.RentalFile.Services;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.App.RentalFile;

[ApiController]
[Route("[controller]")]
public class RentalFileController(ICurrentUserService currentUserService, IRentalFileService rentalFileService) : ControllerBase
{
    /// <summary>
    /// Récupère le dossier de location (rental file) d'un utilisateur via son ID.
    /// </summary>
    /// <returns>Le dossier de location ou un code d'erreur</returns>
    /// <response code="200">Succès : dossier trouvé</response>
    /// <response code="404">Aucun dossier trouvé pour cet utilisateur</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet]
    [ProducesResponseType(typeof(RentalFileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalFileDto>> GetRentalFile()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized("Utilisateur non connecté");
        
        try
        {
            var rentalFile = await rentalFileService.GetRentalFileDetailsByUserId(userId.Value);
            if (rentalFile is null) return NotFound($"Aucun dossier de location trouvé pour l'utilisateur {userId}");

            return Ok(rentalFile);
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur s’est produite lors de la récupération du dossier : {ex.Message}");
        }
    }
}