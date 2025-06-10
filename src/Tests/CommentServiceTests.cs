using Xunit;
using Moq;
using System.Threading.Tasks;
using LodgerBackend.User.Services;
using LodgerBackend.User.Repositories;
using LodgerBackend.User.Models.Entities;

namespace Tests;

public class CommentServiceTests
{
    private readonly Mock<ICommentRepository> _commentRepository = new();

    private CommentService CreateService()
    {
        return new CommentService(_commentRepository.Object);
    }

    [Fact]
    public async Task AddComment_CallsRepositoryAndReturnsComment()
    {
        // Arrange
        int userId = 42;
        string commentText = "Un commentaire de test";
        var expectedComment = new Comment
        {
            Id = 1,
            Text = commentText,
            UserId = userId
        };
        _commentRepository.Setup(r => r.Save(userId, commentText)).ReturnsAsync(expectedComment);

        var service = CreateService();

        // Act
        var result = await service.AddComment(userId, commentText);

        // Assert
        Assert.Equal(expectedComment, result);
        _commentRepository.Verify(r => r.Save(userId, commentText), Times.Once);
    }
}

