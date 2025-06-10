using LodgerBackend.App.User.Models.Entities;

namespace LodgerBackend.App.User.Repositories;

public interface ICommentRepository
{
    Task<Comment> Save(int userId, string comment);
}