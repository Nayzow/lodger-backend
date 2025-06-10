using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Document.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.Document;

[ApiController]
[Route("api/[controller]")]
public class DocumentController(ICurrentUserService currentUserService, IDocumentService documentService) : ControllerBase
{
    /// <summary>
    /// Récupère les documents de l'utilisateur connecté.
    /// </summary>
    /// <returns>Liste des documents.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDocuments()
    {
        var userId = currentUserService.userId;
        if (userId is null) return Unauthorized("Utilisateur non connecté.");

        try
        {
            var documents = await documentService.GetAllByUserId(userId.Value);
            return Ok(documents);
        }
        
        catch (Exception)
        {
            return StatusCode(500, "Une erreur est survenue lors de la récupération des documents.");
        }
    }
}