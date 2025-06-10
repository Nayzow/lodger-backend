using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodgerBackend.User.Models.Entities;

[Table("roles")]
public class Role
{
    [Key]
    [Required]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")] [MaxLength(100)] public string? Name { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; } = new List<UserRole>();

}