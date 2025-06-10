using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.Document.Models;

public class DocumentDto
{
    /// <summary>
    /// Le fichier à renvoyer.
    /// </summary>
    [FromForm(Name = "file_name")]
    [JsonPropertyName("file_name")]
    public required string FileName { get; set; }

    [FromForm(Name = "document_categorie")]
    [JsonPropertyName("document_categorie")]

    public required string DocumentCategorie { get; set; }

    [FromForm(Name = "document_type")]
    [JsonPropertyName("document_type")]

    public required string DocumentType { get; set; }
}