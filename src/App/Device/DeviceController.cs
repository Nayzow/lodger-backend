using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.Device.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.App.Device;

[ApiController]
[Route("api/[controller]")]
public class DeviceController(ICurrentUserService currentUserService, IDeviceService deviceService) : ControllerBase
{
    /// <summary>
    /// Récupère les devices de l'utilisateur connecté.
    /// </summary>
    /// <returns>Liste des devices.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDevices()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized("Utilisateur non connecté.");

        try
        {
            var devices = await deviceService.GetDevicesByUserId(userId.Value);
            return Ok(devices);
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Une erreur est survenue lors de la récupération des paiements.");
        }
    }
}