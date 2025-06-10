using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LodgerBackend.App.User.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.User.Models.Entities;


[Table("users")]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("rental_file_id")]
    [ForeignKey("RentalFileId")]
    [Required]
    public int? RentalFileId { get; set; }
    
    [Column("name")]
    public required string Name { get; set; }

    [Column("firstname")]
    public string? FirstName { get; set; }  

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("email")]
    public required string Email { get; set; }

    [Column("password")]
    public required string Password { get; set; }

    [Column("gender")]
    public EGender Gender { get; set; } = EGender.Homme;

    [Column("nationality")]
    public ENationality Nationality { get; set; } = ENationality.FRANCAIS;
    [Column("is2fa")]
    public bool Is2FA { get; set; }

    [Column("address_id")]
    public int? AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address? Address { get; set; }
    
    public virtual RentalFile.Models.RentalFile? RentalFile { get; set; }

    public ICollection<Settings.Models.Settings> Settings { get; set; } = new List<Settings.Models.Settings>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<Payment.Models.Payment> Payments { get; set; } = new List<Payment.Models.Payment>();
    public ICollection<Document.Models.Document> Documents { get; set; } = new List<Document.Models.Document>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Device.Models.Device> Devices { get; set; } = new List<Device.Models.Device>();
}
