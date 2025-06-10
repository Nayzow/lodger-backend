using LodgerBackend.Auth.Models.Payloads;
using LodgerBackend.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    ICurrentUserService currentUserService,
    IAuthService authService,
    IRefreshTokenService refreshTokenService,
    IPasswordResetService passwordResetService) : ControllerBase
{
    /// <summary>
    /// Authentifie un utilisateur via login/mot de passe.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid) return BadRequest("Données d'identification invalides.");
        var response = await authService.HandleLogin(loginRequest);
        
        return response is null
            ? Unauthorized("Email ou mot de passe non valide")
            : Ok(response);
    }

    /// <summary>
    /// Rafraîchit le token d’authentification à partir du token existant.
    /// </summary>
    [HttpGet("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh()
    {
        try
        {
            if (currentUserService.userId is null) return Unauthorized(new { message = "User ID not found in the token." });
            var authResponse = await refreshTokenService.RefreshAccessTokenByWebUserId(currentUserService.userId.Value);
            
            return Ok(authResponse);
        }
            
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = "Invalid or expired refresh token.", details = ex.Message });
        }
            
        catch (Exception)
        {
            return StatusCode(500, "Erreur lors du rafraîchissement du token.");
        }
    }

    /// <summary>
    /// Création de compte client classique.
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignUp([FromBody] SignupRequest signupRequest)
    {
        if (!ModelState.IsValid) return BadRequest("Requête invalide.");
        if (signupRequest.Password.Length < 8) return BadRequest("Le mot de passe doit comporter au moins 8 caractères.");
        var (success, message) = await authService.HandleSignUp(signupRequest);
        
        return success ? Ok(new { message }) : Unauthorized(message);
    }

    /// <summary>
    /// Réinitialisation de mot de passe à partir d’un token.
    /// </summary>
    [HttpPost("password/reset")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] PerformPasswordResetPayload payload)
    {
        if (!ModelState.IsValid) return BadRequest("Requête invalide.");
        var (success, message) = await passwordResetService.PerformResetAsync(payload);
        
        return success ? Ok(new { message }) : BadRequest(message);
    }

    /// <summary>
    /// Demande de lien de réinitialisation par e-mail.
    /// </summary>
    [HttpPost("password/reset/request")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResetPasswordRequest([FromBody] RequestPasswordResetPayload payload)
    {
        if (!ModelState.IsValid) return BadRequest("Requête invalide.");
        var (success, message) = await passwordResetService.RequestReset(payload);
        
        return success ? Ok(new { message }) : StatusCode(500, message);
    }
}