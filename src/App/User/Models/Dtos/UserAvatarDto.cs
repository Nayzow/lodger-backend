using System.ComponentModel.DataAnnotations;

namespace LodgerBackend.App.User.Models.Dtos;

public class UserAvatarDto
{
    [Required]
    public required IFormFile Image { get; set; }
}

