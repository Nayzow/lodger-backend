using LodgerBackend.Document.Enum;

namespace LodgerBackend.Document.Models;

public class DocumentTypeDto
{
    public EDocumentType Id { get; set; }
    public string Name { get; set; } = null!;
}