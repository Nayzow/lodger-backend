using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodgerBackend.App.Auth.Models.Entities;

[Table("reset_password_request")]
public class ResetPasswordRequest
{
    public int Id { get; set; }
    [Column("user_id")] public int UserId { get; set; }
    [MaxLength(10000)] public string? Token { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}