using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodgerBackend.Device.Models;

[Table("devices")]
public class Device
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string? Name { get; set; }

    [Column("ip_address")]
    [Required]
    public required string IpAddress { get; set; }

    [Column("type")]
    [Required]
    public required string Type { get; set; }

    [Column("location")]
    [Required]
    public required string Location { get; set; }

    [Column("date")]
    [Required]
    public DateTime Date { get; set; }

    public virtual User.Models.Entities.User? User { get; set; }
}