using System.ComponentModel.DataAnnotations;

namespace LodgerBackend.User.Models.Dtos;

public class UserAvatarDto
{
    [Required]
    public required IFormFile Image { get; set; }
}

