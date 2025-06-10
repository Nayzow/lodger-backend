using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.User.Models.Entities;

[Table("user_roles")]
[Keyless]
public class UserRole
{
    [Column("user_id")]
    [Required]
    public required int UserId { get; set; }

    [Column("role_id")]
    [Required]
    public required int RoleId { get; set; }

    [ForeignKey("UserId")]
    public required User User { get; set; }

    [ForeignKey("RoleId")]
    public required Role Role { get; set; }
}