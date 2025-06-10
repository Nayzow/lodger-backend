using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.Device.Models;
using LodgerBackend.App.Device.Services;
using LodgerBackend.App.Document.Services;
using LodgerBackend.App.File.Models;
using LodgerBackend.App.RentalFile;
using LodgerBackend.App.RentalFile.Models;
using LodgerBackend.App.RentalFile.Services;
using LodgerBackend.App.Settings;
using LodgerBackend.App.User.Models.Dtos;
using LodgerBackend.App.User.Services;
using LodgerBackend.src.App.Settings.Dtos;
using LodgerBackend.src.App.User.PDF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace LodgerBackend.App.User;

[ApiController]
[Route("api/users/me")]
public class UserController(
    IUserService userService,
    IPasswordResetService passwordResetService,
    IDeviceService deviceService,
    IDocumentService documentService,
    IRentalFileService rentalFileService,
    ISettingsService settingsService,
    ICurrentUserService currentUserService) : ControllerBase
{

    /// <summary>
    /// Récupère les détails d'un utilisateur par son identifiant.
    /// </summary>
    /// <returns>Données complètes de l'utilisateur</returns>
    /// <response code="200">Utilisateur trouvé</response>
    /// <response code="400">Paramètre userId invalide</response>
    /// <response code="404">Utilisateur non trouvé</response>
    /// <response code="500">Erreur interne serveur</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserWithDetailsAsync()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();

        try
        {
            var userDto = await userService.GetUserWithDetailsAsync(userId.Value);

            return Ok(userDto);
        }
        catch
        {
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }
    /// <summary>
    /// Met à jour les détails d'un utilisateur par son identifiant.
    /// </summary>
    /// <returns>Données complètes de l'utilisateur</returns>
    /// <response code="200">Utilisateur trouvé</response>
    /// <response code="400">Paramètre userId invalide</response>
    /// <response code="404">Utilisateur non trouvé</response>
    /// <response code="409">Email déjà utilisé par un autre utilisateur</response>
    /// <response code="500">Erreur interne serveur</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserWithDetailsAsync([FromBody] UserDetailsDto userDetailsDto)
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();

        try
        {
            if (!string.IsNullOrWhiteSpace(userDetailsDto.Email))
            {
                var existingUser = await userService.GetByEmail(userDetailsDto.Email);

                if (existingUser != null && existingUser.Id != userId)
                {
                    return Conflict($"L'email '{userDetailsDto.Email}' est déjà utilisé par un autre compte.");
                }
            }
            var user = await userService.GetById(userId.Value);

            if (user == null)
            {
                return NotFound($"Aucun utilisateur trouvé avec l'identifiant {userId}.");
            }

            userDetailsDto = await userService.UpdateUserWithDetailsAsync(userDetailsDto, user);


            return Ok(userDetailsDto);
        }
        catch
        {
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }

    /// <summary>
    /// Permet à un utilisateur authentifié de changer son mot de passe.
    /// Retourne les erreurs de validation si la requête est invalide.
    /// </summary>
    /// <param name="payload">Objet contenant l'ancien et le nouveau mot de passe.</param>
    /// <returns>Résultat de la modification du mot de passe.</returns>
    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK avec un message
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // BadRequest avec liste d'erreurs
    [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Unauthorized simple
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Erreur serveur
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto payload)
    {
        var userId = currentUserService.userId;
        if (userId is null)
            return Unauthorized("Utilisateur non authentifié.");

        if (!ModelState.IsValid)
        {
            var error = ModelState.First(ms => ms.Value?.Errors.Count > 0);

            return BadRequest(error);
        }

        try
        {
            var (success, message) = await passwordResetService.ChangePassword(userId.Value, payload);

            return success
                ? Ok(message)
                : BadRequest(message);
        }
        catch (Exception)
        {
            // Tu peux logger ici si besoin
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }

    /// <summary>
    /// Récupère les devices connecté de l'utilisateur.
    /// </summary>
    /// <returns>Données complètes de l'utilisateur</returns>
    /// <response code="200">Utilisateur trouvé</response>
    /// <response code="400">Paramètre userId invalide</response>
    /// <response code="404">Utilisateur non trouvé</response>
    /// <response code="500">Erreur interne serveur</response>
    [HttpGet("devices")]
    [Authorize]
    [ProducesResponseType(typeof(List<DeviceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserDevicesAsync()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();
        try
        {
            var devicesDto = await deviceService.GetDevicesByUserId(userId.Value);
            return Ok(devicesDto);
        }
        catch
        {
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }

    /// <summary>
    /// Supprime un appareil spécifique pour l'utilisateur connecté.
    /// </summary>
    /// <param name="deviceId">Identifiant de l'appareil à supprimer.</param>
    /// <returns>Retourne 200 OK si la suppression est réussie, 400 BadRequest sinon.</returns>
    /// <response code="200">Appareil supprimé avec succès.</response>
    /// <response code="400">Erreur lors de la suppression de l'appareil.</response>
    /// <response code="401">Utilisateur non autorisé.</response>
    [HttpDelete("devices/{deviceId:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK avec un message
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // BadRequest avec liste d'erreurs
    [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Unauthorized simple
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Erreur serveur
    public async Task<IActionResult> DeleteDevice(int deviceId)
    {
        var userId = currentUserService.userId;

        if (userId is null)
        {
            return Unauthorized();
        }

        try
        {
            // TODO : Connecté un jeton à l'appareil
            var (success, message) = await deviceService.DeleteDeviceId(userId.Value, deviceId);

            return success
                ? Ok(message)
                : BadRequest(message);
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Une erreur interne est survenue." });
        }
    }
    
    /// <summary>
    /// Upload un fichier.
    /// </summary>
    /// <param name="request">L’objet contenant le fichier.</param>
    /// <returns>Un message de confirmation.</returns>
    [HttpPost("documents")]
    [Consumes("multipart/form-data")]
    [ApiExplorerSettings(IgnoreApi = true)] // Ne sera pas inclus dans Swagger
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Upload([FromForm] UploadFilesRequest request)
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();
        
        try
        {
            var (success, message) = await documentService.UploadFilesAsync(userId.Value, request.Files);

            return success
                    ? Ok(message)
                    : BadRequest(message);
        }

        catch (Exception)
        {
            return StatusCode(500, "Une erreur est survenue lors de des documents.");
        }
    }

    /// <summary>
    /// Télécharge un fichier par son nom.
    /// </summary>
    /// <param name="fileName">Le nom du fichier à télécharger.</param>
    /// <returns>Le fichier à télécharger.</returns>
    [HttpGet("documents/{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Download(string fileName)
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();

        try
        {
            var result = await documentService.DownloadFile(fileName, userId.Value);

            return result;
        }

        catch (Exception)
        {
            return StatusCode(500, $"Une erreur est survenue lors du téléchargement du fichier portant le : {fileName}.");
        }
    }


    /// <summary>
    /// Récupère les détails du dossier de l'utilisateur.
    /// </summary>
    /// <returns>Données complètes de l'utilisateur</returns>
    /// <response code="200">Utilisateur trouvé</response>
    /// <response code="400">Paramètre userId invalide</response>
    /// <response code="404">Utilisateur non trouvé</response>
    /// <response code="500">Erreur interne serveur</response>
    [HttpGet("dossier")]
    [Authorize]
    [ProducesResponseType(typeof(RentalFileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRentalFile()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();

        try
        {
            var rentalFileDto = await rentalFileService.GetRentalFileDetailsByUserId(userId.Value);

            if (rentalFileDto is null)
            {
                return NotFound($"Aucun dossier n'a été trouvé pour l'utilisateur : {userId}.");
            }

            return Ok(rentalFileDto);
        }
        catch
        {
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }
    /// <summary>
    /// Met à jour les détails d'un utilisateur par son identifiant.
    /// </summary>
    /// <returns>Données complètes de l'utilisateur</returns>
    /// <response code="200">Utilisateur trouvé</response>
    /// <response code="400">Paramètre userId invalide</response>
    /// <response code="404">Utilisateur non trouvé</response>
    /// <response code="409">Email déjà utilisé par un autre utilisateur</response>
    /// <response code="500">Erreur interne serveur</response>
    [HttpPut("dossier")]
    [Authorize]
    [ProducesResponseType(typeof(RentalFileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateRentalFile([FromForm] UpdateRentalFileDto updateRentalFileDto)
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();

        try
        {
            var rentalFile = await rentalFileService.GetRentalFileByUserId(userId.Value);

            if (rentalFile is null)
            {
                return NotFound($"Aucun Dossier n'a été trouvé pour l'utilisateur : {userId}.");
            }

            var newRentalFileDto = await rentalFileService.UpdateRentalFileAsync(userId.Value, updateRentalFileDto, rentalFile);


            return Ok(newRentalFileDto);
        }
        catch
        {
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }

    /// <summary>
    /// Exporte les données personnelles de l'utilisateur connecté au format PDF.
    /// </summary>
    /// <returns>Un fichier PDF contenant les données personnelles</returns>
    /// <response code="200">PDF généré avec succès</response>
    /// <response code="401">Utilisateur non authentifié</response>
    /// <response code="404">Utilisateur introuvable</response>
    /// <response code="500">Erreur interne lors de la génération du PDF</response>
    [HttpGet("export")]
    [Authorize]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExportUserDataAsPdf()
    {
        var userId = currentUserService.userId;
        if (userId is null)
            return Unauthorized();

        try
        {
            var user = await userService.GetUserWithDetailsAsync(userId.Value);
            if (user == null)
                return NotFound("Utilisateur introuvable.");

            var pdfStream = new MemoryStream();
            var document = new UserExportPdf(user);
            document.GeneratePdf(pdfStream);

            pdfStream.Position = 0;
            return File(pdfStream, "application/pdf", "export-rgpd.pdf");
        }
        catch (Exception ex)
        {
            // Log ex si nécessaire
            return StatusCode(500, "Une erreur interne est survenue lors de la génération du PDF.");
        }
    }

    /// <summary>
    /// Supprime définitivement le compte de l'utilisateur connecté.
    /// </summary>
    /// <response code="204">Utilisateur supprimé avec succès</response>
    /// <response code="401">Utilisateur non authentifié</response>
    /// <response code="500">Erreur interne lors de la suppression</response>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCurrentUserAsync()
    {
        var userId = currentUserService.userId;
        if (userId is null)
            return Unauthorized();

        try
        {
            await userService.DeleteUserByIdAsync(userId.Value);
            return NoContent(); // 204
        }
        catch (Exception)
        {
            // _logger.LogError(ex, "Erreur suppression utilisateur {userId}", userId);
            return StatusCode(500, "Une erreur est survenue lors de la suppression du compte.");
        }
    }

    /// <summary>
    /// Récupère les paramètres de l'utilisateur.
    /// </summary>
    /// <returns>Paramètres de l'utilisateur</returns>
    /// <response code="200">Paramètres récupérés avec succès</response>
    /// <response code="400">Paramètre userId invalide</response>
    /// <response code="404">Paramètres non trouvés</response>
    /// <response code="500">Erreur interne serveur</response>
    [HttpGet("settings")]
    [Authorize]
    [ProducesResponseType(typeof(SettingsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSettingsAsync()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();

        try
        {
            var settingsDto = await settingsService.GetSettingsAsync(userId.Value);

            if (settingsDto == null)
            {
                return NotFound("Aucun paramètre trouvé pour cet utilisateur.");
            }

            return Ok(settingsDto);
        }
        catch
        {
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }
    /// <summary>
    /// Met à jour les paramètres de l'utilisateur.
    /// </summary>
    /// <returns>Paramètres mis à jour</returns>
    /// <response code="200">Paramètres mis à jour avec succès</response>
    /// <response code="400">Paramètre userId invalide</response>
    /// <response code="404">Paramètres non trouvés</response>
    /// <response code="500">Erreur interne serveur</response>
    [HttpPut("settings")]
    [Authorize]
    [ProducesResponseType(typeof(UpdateSettingsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSettingsAsync([FromBody] UpdateSettingsDto newSettingsDto)
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized();

        try
        {
            var settingsDto = await settingsService.UpdateSettingsAsync(userId.Value, newSettingsDto);

            return Ok(settingsDto);
        }
        catch
        {
            return StatusCode(500, "Une erreur interne est survenue.");
        }
    }




}




