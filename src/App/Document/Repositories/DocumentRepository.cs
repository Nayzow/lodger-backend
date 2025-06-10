using LodgerBackend.App.Configuration.DbContext;
using LodgerBackend.App.Document.Enum;
using LodgerBackend.App.User.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.Document.Repositories;

public class DocumentRepository(LodgerDbContext dbContext) : IDocumentRepository
{
    public async Task<List<Models.Document>> GetAllByUserId(int userId)
    {
        return await dbContext.Documents
            .Where(document => document.Id == userId)
            .ToListAsync();
    }

    public async Task<List<Models.Document>> GetDocuementsByUserIdAndByDocumentType(int userId, EDocumentType documentType)
    {
        return await dbContext.Documents
            .Where(d => d.UserId == userId && d.DocumentTypeId == documentType)
            .ToListAsync();
    }

    public async Task<Models.Document?> GetDocumentByTypeAndCategorie(EDocumentCategorie eDocumentCategorie, EDocumentType eDocumentType, int userId)
    {
        return await dbContext.Documents.Where(doc =>
            doc.DocumentTypeId == eDocumentType &&
            doc.UserId == userId &&
            doc.DocumentCategorieId == eDocumentCategorie
        ).FirstOrDefaultAsync();
    }

    public async Task<Models.Document> Save(Models.Document document)
    {
        if (document.Id == 0)
        {
            dbContext.Documents.Add(document);
        }
        else
        {
            dbContext.Documents.Update(document);
        }

        await dbContext.SaveChangesAsync();
        return document;
    }
}