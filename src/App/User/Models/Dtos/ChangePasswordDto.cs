using System.ComponentModel.DataAnnotations;

namespace LodgerBackend.App.User.Models.Dtos;

public class ChangePasswordDto
{
    [Required]
    public required string Password { get; set; }

    [Required]
    public required string NewPassword { get; set; }

    [Required]
    [Compare("NewPassword", ErrorMessage = "Les mots de passe ne correspondent pas.")]
    public required string ConfirmNewPassword { get; set; }
}


