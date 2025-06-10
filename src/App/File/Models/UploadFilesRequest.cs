using LodgerBackend.File.dtos;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.File.Models;

public class UploadFilesRequest
{
    /// <summary>
    /// Les fichiers à uploader.
    /// </summary>
    [FromForm(Name = "files")]
    public required List<UploadFileDto> Files { get; set; }
}
