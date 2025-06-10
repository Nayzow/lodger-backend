using AutoMapper;
using LodgerBackend.App.Document.Enum;
using LodgerBackend.App.Document.Models;
using LodgerBackend.App.Document.Repositories;
using LodgerBackend.App.File.dtos;
using LodgerBackend.App.File.Services;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.App.Document.Services;

public class DocumentService(
    IDocumentRepository documentRepository,
    IFileService fileService,
    IMapper mapper,
    ILogger<DocumentService> logger) : IDocumentService
{
    public async Task<List<DocumentDto>> GetAllByUserId(int userId)
    {
        try
        {
            logger.LogInformation("Fetching documents for user with ID {UserId}", userId);
            var documents = await documentRepository.GetAllByUserId(userId);
            var documentsDto = mapper.Map<List<DocumentDto>>(documents);

            logger.LogInformation("{Count} documents fetched for user {UserId}", documentsDto.Count, userId);
            
            return documentsDto;
        }
        
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching documents for user {UserId}", userId);
            throw;
        }
    }

    public async Task<List<DocumentDto>> GetDocumentsByUserIdAndByDocumentType(int userId, EDocumentType documentType)
    {
        var files = await documentRepository.GetDocuementsByUserIdAndByDocumentType(userId, documentType);
        var filesDto = mapper.Map<List<DocumentDto>>(files);
        
        return filesDto;
    }

    public Task<FileStreamResult> DownloadFile(string fileName, int userId)
    {
        return fileService.DownloadFileAsync(fileName, userId);
    }

    public async Task<Models.Document?> GetOneDocumentByTypeAndCategorie(int userId, UploadFileDto file)
    {
        var documentType = DocumentTypeExtensions.FromName(file.DocumentType);
        var documentCategorie = DocumentCategorieExtensions.FromName(file.DocumentCategorie);
        var document = await documentRepository.GetDocumentByTypeAndCategorie(documentCategorie, documentType, userId);

        if(document is not null && document.Name == file.File.FileName) return document; 
        // TODO: Créer une méthode qui vérifie vraiment si le fichier est identique

        await fileService.UploadFileAsync(file, userId);
        document = new Models.Document
        {
            UserId = userId,
            Name = file.File.FileName,
            FileUrl = "", // TODO: a renseigner dans appsettings pour la baseUrl 
            DocumentTypeId = documentType,
            DocumentCategorieId = documentCategorie,
        };
        document = await documentRepository.Save(document);
        return document;
    }
    public async Task<(bool Success, string Message)> UploadFilesAsync(int userId, List<UploadFileDto> files)
    {

        foreach (var file in files)
        {
            try
            {
                await fileService.UploadFileAsync(file, userId);
                var document = await GetOneDocumentByTypeAndCategorie(userId, file);

                if (document is null) return (false, $"Erreur pour récupérer le fichier : {file.DocumentCategorie}"); 
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erreur lors de l'upload du fichier '");
                return (false, $"Erreur lors de l'upload du fichier : {file.DocumentCategorie}");
            }
        }
        return (true, "Fichiers uploadés");
    }
}