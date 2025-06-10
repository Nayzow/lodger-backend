using AutoMapper;
using LodgerBackend.App.Configuration.DbContext;
using LodgerBackend.App.Document.Repositories;
using LodgerBackend.App.Document.Services;
using LodgerBackend.App.File.Services;
using LodgerBackend.App.User.Models.Dtos;
using LodgerBackend.App.User.Models.Entities;
using LodgerBackend.App.User.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.User.Services;

public class CommentService(ICommentRepository commentRepository) : ICommentService
{
    public async Task<Comment> AddComment(int userId, string comment)
    {
        return await commentRepository.Save(userId, comment);
    }

    
}
