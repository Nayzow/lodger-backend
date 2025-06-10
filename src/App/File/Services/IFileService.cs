using LodgerBackend.App.File.dtos;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.App.File.Services;

public interface IFileService
{
    Task UploadFileAsync(UploadFileDto file, int userId);
    Task<FileStreamResult> DownloadFileAsync(string fileName, int userId);
    Task<string> DownloadFileAsBase64Async(string fileName);
}