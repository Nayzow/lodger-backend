using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.File.dtos;
using LodgerBackend.App.File.Services;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.App.File;

[ApiController]
[Route("api/[controller]")]
public class FileController(ICurrentUserService currentUserService, IFileService fileService) : ControllerBase
{
    /// <summary>
    /// Upload un fichier.
    /// </summary>
    /// <param name="request">L’objet contenant le fichier.</param>
    /// <returns>Un message de confirmation.</returns>
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Upload([FromForm] UploadFileDto request)
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized("Utilisateur non connecté.");

        try
        {
            await fileService.UploadFileAsync(request, userId.Value);
            return Ok("Fichier uploadé avec succès.");
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Une erreur est survenue lors de l'upload.");
        }
    }

    /// <summary>
    /// Télécharge un fichier par son nom.
    /// </summary>
    /// <param name="fileName">Le nom du fichier à télécharger.</param>
    /// <returns>Le fichier à télécharger.</returns>
    [HttpGet("download/{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Download(string fileName)
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized("Utilisateur non connecté.");
        
        try
        {
            var result = await fileService.DownloadFileAsync(fileName, userId.Value);
            return result;
        }
        
        catch (FileNotFoundException)
        {
            return NotFound("Fichier introuvable.");
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Une erreur est survenue lors du téléchargement.");
        }
    }
}
