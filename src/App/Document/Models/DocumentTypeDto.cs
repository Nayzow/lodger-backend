using LodgerBackend.App.Document.Enum;

namespace LodgerBackend.App.Document.Models;

public class DocumentTypeDto
{
    public EDocumentType Id { get; set; }
    public string Name { get; set; } = null!;
}