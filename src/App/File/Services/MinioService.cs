using System.Text;
using LodgerBackend.File.dtos;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace LodgerBackend.File.Services;

public class MinioService(IMinioClient minioClient, ILogger<MinioService> logger) : IFileService
{
    private const string BucketName = "lodger-bucket";

    public async Task UploadFileAsync(UploadFileDto uploadFileDto, int userId)
    {
        var file = uploadFileDto.File;
        var fileKey = $"{userId}/{file.FileName}";

        try
        {
            await EnsureUserFolderAsync(userId);

            await using var stream = file.OpenReadStream();
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(BucketName)
                .WithObject(fileKey)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);

            await minioClient.PutObjectAsync(putObjectArgs);

            logger.LogInformation("Fichier {FileName} uploadé avec succès dans le bucket {Bucket} pour l'utilisateur {UserId}.",
                file.FileName, BucketName, userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'upload du fichier {FileName} pour l'utilisateur {UserId}.", file.FileName, userId);
            throw; // Propagation pour traitement dans le contrôleur
        }
    }

    public async Task<FileStreamResult> DownloadFileAsync(string fileName, int userId)
    {
        var fileKey = $"{userId}/{fileName}";
        var memoryStream = new MemoryStream();

        try
        {
            var getObjectArgs = new GetObjectArgs()
                .WithBucket(BucketName)
                .WithObject(fileKey)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream));

            await minioClient.GetObjectAsync(getObjectArgs);
            memoryStream.Position = 0;

            logger.LogInformation("Fichier {FileName} téléchargé avec succès pour l'utilisateur {UserId}.", fileKey, userId);

            return new FileStreamResult(memoryStream, "application/octet-stream")
            {
                FileDownloadName = fileName
            };
        }
        catch (ObjectNotFoundException)
        {
            logger.LogWarning("Fichier {FileName} introuvable pour l'utilisateur {UserId}.", fileKey, userId);
            throw new FileNotFoundException("Fichier introuvable.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors du téléchargement du fichier {FileName} pour l'utilisateur {UserId}.", fileKey, userId);
            throw;
        }
    }

    public async Task<string> DownloadFileAsBase64Async(string fileName)
    {
        var memoryStream = new MemoryStream();

        try
        {
            var getObjectArgs = new GetObjectArgs()
                .WithBucket(BucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream));

            await minioClient.GetObjectAsync(getObjectArgs);
            memoryStream.Position = 0;
            var fileBytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(fileBytes);

            logger.LogInformation("Fichier {FileName} téléchargé en base64 avec succès.", fileName);

            return base64String;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors du téléchargement en base64 du fichier {FileName}.", fileName);
            throw;
        }
    }

    private async Task EnsureUserFolderAsync(int userId)
    {
        var folderKey = $"{userId}/";

        try
        {
            var statObjectArgs = new StatObjectArgs()
                .WithBucket(BucketName)
                .WithObject(folderKey);

            await minioClient.StatObjectAsync(statObjectArgs);
        }
        catch (ObjectNotFoundException)
        {
            try
            {
                // Ajoute un faux contenu (ex: un espace vide)
                var contentBytes = Encoding.UTF8.GetBytes(" ");
                var emptyStream = new MemoryStream(contentBytes);

                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(BucketName)
                    .WithObject(folderKey)
                    .WithStreamData(emptyStream)
                    .WithObjectSize(contentBytes.Length)
                    .WithContentType("application/octet-stream");

                await minioClient.PutObjectAsync(putObjectArgs);

                logger.LogInformation("Dossier utilisateur {UserId} créé dans le bucket {Bucket}.", userId, BucketName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erreur lors de la création du dossier utilisateur {UserId}.", userId);
                throw;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la vérification du dossier utilisateur {UserId}.", userId);
            throw;
        }
    }
}
