using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodgerBackend.App.Auth.Models.Entities;

[Table("refresh_tokens")]
public class RefreshToken
{
    [Key]
    [Column("id")]
    [Required]
    public int Id { get; set; }
    
    [Column("user_id")] public int UserId { get; set; }
    [Column("token")] [MaxLength(500)] public required string Token { get; set; }
    [Column("expiration")] public DateTime Expiration { get; set; }
    [Column("acces_token_expiration")] public DateTime AccesTokenExpiration { get; set; }
    [Column("is_revoked")] public bool IsRevoked { get; set; }
}