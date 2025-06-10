using LodgerBackend.User.Models.Entities;
using LodgerBackend.User.Repositories;

namespace LodgerBackend.User.Services;

public class CommentService(ICommentRepository commentRepository) : ICommentService
{
    public async Task<Comment> AddComment(int userId, string comment)
    {
        return await commentRepository.Save(userId, comment);
    }

    
}
