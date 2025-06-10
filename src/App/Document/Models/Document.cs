using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LodgerBackend.App.Document.Enum;
using LodgerBackend.App.User.Models.Entities;


namespace LodgerBackend.App.Document.Models;

[Table("documents")]
public class Document
{
    #region Colonne 
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("user_id")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }


    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("file_url")]
    public string FileUrl { get; set; } = string.Empty;

    [Required]
    [Column("document_type_id")]
    //[ForeignKey(nameof(DocumentType))]
    public EDocumentType DocumentTypeId { get; set; } = EDocumentType.ETAT_DES_LIEUX;

    [Required]
    [Column("document_categorie_id")]
    public EDocumentCategorie DocumentCategorieId { get; set; } = EDocumentCategorie.PASSPORT_OU_CI;

    #endregion

    #region Table Etrangère
    //[ForeignKey(nameof(DocumentTypeId))]
    //public virtual DocumentType? DocumentType { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User.Models.Entities.User? User { get; set; }

    #endregion

    #region Table Jointure
    #endregion
}