using LodgerBackend.Configuration.DbContext;
using LodgerBackend.User.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.User.Repositories;

public class AddressRepository(LodgerDbContext dbContext) : IAddressRepository
{

    public async Task<User.Models.Entities.User> Save(User.Models.Entities.User user)
    {
        if (user.Id == 0)
        {
            dbContext.Users.Add(user);
        }
        else
        {
            dbContext.Users.Update(user);
        }

        await dbContext.SaveChangesAsync();
        return user;
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