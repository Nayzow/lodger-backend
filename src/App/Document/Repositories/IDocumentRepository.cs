using LodgerBackend.Document.Enum;

namespace LodgerBackend.Document.Repositories;

public interface IDocumentRepository
{
    Task<List<Models.Document>> GetAllByUserId(int userId);
    Task<List<Models.Document>> GetDocuementsByUserIdAndByDocumentType(int userId, EDocumentType documentType);
    Task<Models.Document?> GetDocumentByTypeAndCategorie(EDocumentCategorie eDocumentCategorie, EDocumentType eDocumentType, int userId);
    Task<Models.Document> Save(Models.Document document);
}