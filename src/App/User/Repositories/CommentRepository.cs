using LodgerBackend.Configuration.DbContext;
using LodgerBackend.User.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.User.Repositories;

public class CommentRepository(LodgerDbContext dbContext) : ICommentRepository
{

    public async Task<Comment> Save(int userId, string comment)
    {
        var newComment = new Comment { Text = comment, UserId = userId };
        dbContext.Comments.Add(newComment);
        await dbContext.SaveChangesAsync();

        return newComment;
    }

    public async Task<Address?> GetById(int addressId)
    {
        return await dbContext.Addresses.FirstOrDefaultAsync(address => address.Id == addressId);
    }

    public async Task<Address> Save(Address newAddress)
    {
        dbContext.Addresses.Add(newAddress);
        await dbContext.SaveChangesAsync();
        return newAddress;
    }

    public async Task<Address?> GetByAddress(string addressName, string postalCode)
    {
        return await dbContext.Addresses
            .Where(address => address.AddressName == addressName && address.PostalCode == postalCode)
            .FirstOrDefaultAsync();
    }
}