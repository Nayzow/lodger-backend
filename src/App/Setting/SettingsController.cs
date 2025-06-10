using LodgerBackend.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.Setting;

[ApiController]
[Route("api/[controller]")]
public class SettingsController(
    ICurrentUserService currentUserService,
    ISettingsService settingsService) : ControllerBase
{
    /// <summary>
    /// Récupère les paramètres de l'utilisateur connecté.
    /// </summary>
    /// <returns>Les paramètres utilisateur.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSettings()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized("Utilisateur non connecté.");

        try
        {
            var settings = await settingsService.GetSettingsByUserId(userId.Value);

            return settings is not null ? Ok(settings) : NotFound("Paramètre non trouvés.");
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Une erreur est survenue lors de la récupération des paramètres.");
        }
    }
}