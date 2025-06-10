using LodgerBackend.User.Models.Entities;

namespace LodgerBackend.User.Services;

public interface ICommentService
{
    Task<Comment> AddComment(int userId, string comment);
}