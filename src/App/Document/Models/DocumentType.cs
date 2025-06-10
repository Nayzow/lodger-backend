using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LodgerBackend.App.Document.Enum;

namespace LodgerBackend.App.Document.Models;

[Table("document_type")]
public class DocumentType
{
    [Key]
    [Column("id")]
    public EDocumentType Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("code")]
    public string Code { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = null!;

    // Navigation property
    //public ICollection<Document>? Documents { get; set; }

}
