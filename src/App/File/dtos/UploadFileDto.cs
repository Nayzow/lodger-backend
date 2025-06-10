using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.File.dtos;

public class UploadFileDto
{

    /// <summary>
    /// Le fichier à uploader.
    /// </summary>
    [FromForm(Name = "file")]
    public required IFormFile File { get; set; }

    [FromForm(Name = "document_categorie")]
    public required string DocumentCategorie {  get; set; }

    [FromForm(Name = "document_type")]
    public required string DocumentType { get; set; }
}