using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LodgerBackend.App.User.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.User.Models.Entities;


[Table("comments")]
public class Comment
{
    #region Colonne 
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("comment")]
    public required string Text { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    #endregion
    #region Clé étrangère

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    [Required]
    public int UserId { get; set; }
    #endregion
    
    public virtual User? User { get; set; }
}
