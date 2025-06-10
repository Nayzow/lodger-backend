using LodgerBackend.File.dtos;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.File.Models;

public class UploadFileRequest
{
    /// <summary>
    /// Le fichier à uploader.
    /// </summary>
    [FromForm(Name = "file")]
    public UploadFileDto File { get; set; } = null!;
}
