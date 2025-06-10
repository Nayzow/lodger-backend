
using LodgerBackend.App.Document.Enum;
using LodgerBackend.App.Document.Models;
using LodgerBackend.App.File.dtos;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.App.Document.Services;

public interface IDocumentService
{
    Task<List<Models.DocumentDto>> GetAllByUserId(int userId);
    Task<List<DocumentDto>> GetDocumentsByUserIdAndByDocumentType(int userId, EDocumentType documentType);
    Task<FileStreamResult> DownloadFile(string fileName, int userId);
    Task<Models.Document?> GetOneDocumentByTypeAndCategorie(int userId, UploadFileDto file);
    Task<(bool Success, string Message)> UploadFilesAsync(int userId, List<UploadFileDto> files);
}