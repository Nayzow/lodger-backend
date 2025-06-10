using LodgerBackend.App.User.Models.Dtos;
using LodgerBackend.App.User.Models.Entities;

namespace LodgerBackend.App.User.Services;

public interface ICommentService
{
    Task<Comment> AddComment(int userId, string comment);
}