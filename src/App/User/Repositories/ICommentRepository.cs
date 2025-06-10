using LodgerBackend.User.Models.Entities;

namespace LodgerBackend.User.Repositories;

public interface ICommentRepository
{
    Task<Comment> Save(int userId, string comment);
}