using AutoMapper;
using LodgerBackend.Configuration.DbContext;
using LodgerBackend.Document.Enum;
using LodgerBackend.Document.Services;
using LodgerBackend.RentalFile.Models;
using LodgerBackend.RentalFile.Repositories;
using LodgerBackend.User.Services;
using Microsoft.IdentityModel.Tokens;

namespace LodgerBackend.RentalFile.Services;

public class RentalFileService(IRentalFileRepository rentalFileRepository,
    IDocumentService documentService,
    ICommentService commentService,
    LodgerDbContext lodgerDbContext,
    ILogger<RentalFileService> logger,
    IMapper mapper) : IRentalFileService
{
    public async Task<Models.RentalFile> AddRentalFile(Models.RentalFile rentalFile)
    {
        rentalFile = await rentalFileRepository.AddRentalFile(rentalFile);
        return rentalFile;
    }

    public async Task<Models.RentalFile?> GetRentalFileByUserId(int userId)
    {
        return await rentalFileRepository.GetRentalFileByUserId(userId);
    }

    public async Task<RentalFileDto?> GetRentalFileDetailsByUserId(int userId)
    {
        var rentalFile = await rentalFileRepository.GetRentalFileByUserId(userId);

        var rentalFileDto = mapper.Map<RentalFileDto>(rentalFile);
        rentalFileDto.Files = await documentService.GetDocumentsByUserIdAndByDocumentType(userId, EDocumentType.DOSSIER);

        return rentalFileDto;
    }

    public async Task<RentalFileDto?> UpdateRentalFileAsync(int userId, UpdateRentalFileDto rentalFileDto, Models.RentalFile rentalFile)
    {
        using var transaction = await lodgerDbContext.Database.BeginTransactionAsync();

        try
        {

            var updateRentalFile = mapper.Map(rentalFileDto, rentalFile);

            if(!rentalFileDto.Files.IsNullOrEmpty())
            {
                await documentService.UploadFilesAsync(userId, rentalFileDto.Files);

            }
            if(rentalFileDto.Comment is not null)
            { 
                await commentService.AddComment(userId, rentalFileDto.Comment);
            }
            updateRentalFile = await rentalFileRepository.Save(updateRentalFile);

            var newUserDetailsDto = await GetRentalFileDetailsByUserId(userId);
            await transaction.CommitAsync();

            return newUserDetailsDto;


        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            if(rentalFile.User is not null)
            {
                logger.LogError(ex, "Erreur lors de l'enregistrement de l'utilisateur {Email}", rentalFile.User.Email);
            }
            throw;
        }
    }
}