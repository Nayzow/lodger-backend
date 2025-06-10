using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LodgerBackend.Setting.Enums;

namespace LodgerBackend.Setting.Models;

[Table("settings")]
public class Settings
{
    public Settings(int userId, ENotification theme, bool notificationsEnabled)
    {
        UserId = userId;
        Theme = theme;
        NotificationsEnabled = notificationsEnabled;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [Column("theme")]
    public ENotification Theme { get; set; } = ENotification.Email;

    [Column("notifications_enabled")]
    public bool NotificationsEnabled { get; set; } = true;

    [Column("language")]
    public string Language { get; set; } = "en";
    
    public virtual User.Models.Entities.User? User { get; set; }
}