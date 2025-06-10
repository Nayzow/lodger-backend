using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodgerBackend.User.Models.Entities;


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
    #region Cl� �trang�re

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    [Required]
    public int UserId { get; set; }
    #endregion
    
    public virtual User? User { get; set; }
}
