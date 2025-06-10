namespace LodgerBackend.App.User.Repositories;

public interface IAddressRepository
{
    Task<Models.Entities.Address?> GetById(int addressId);
    Task<Models.Entities.Address?> GetByAddress(string address, string postalCode);
    Task<Models.Entities.Address> Save(Models.Entities.Address newAddress);
}