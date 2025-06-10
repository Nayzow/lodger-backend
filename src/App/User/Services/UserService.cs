using AutoMapper;
using LodgerBackend.App.Configuration.DbContext;
using LodgerBackend.App.Document.Services;
using LodgerBackend.App.User.Models.Dtos;
using LodgerBackend.App.User.Models.Entities;
using LodgerBackend.App.User.Repositories;

namespace LodgerBackend.App.User.Services;

public class UserService(
    IUserRepository userRepository,
    IAddressRepository addressRepository,
    ILogger<UserService> logger,
    LodgerDbContext lodgerDbContext,
    IMapper mapper) : IUserService
{
    public async Task<UserDetailsDto> GetUserWithDetailsAsync(int userId)
    {
        try
        {
            logger.LogInformation("Récupération des détails de l'utilisateur {UserId}", userId);
            var user = await userRepository.GetUserWithDetailsAsync(userId);
            
            if (user == null)
                logger.LogWarning("Aucun utilisateur trouvé avec l'ID {UserId}", userId);
            else
                logger.LogInformation("Détails de l'utilisateur {UserId} récupérés avec succès", userId);

            var userDto = mapper.Map<UserDetailsDto>(user);

            return userDto;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération de l'utilisateur {UserId}", userId);
            throw;
        }
    }

    public async Task<Models.Entities.User?> GetByEmail(string email)
    {
        try
        {
            logger.LogInformation("Recherche de l'utilisateur avec l'email {Email}", email);
            var user = await userRepository.GetByEmail(email);

            if (user == null)
                logger.LogWarning("Aucun utilisateur trouvé avec l'email {Email}", email);
            else
                logger.LogInformation("Utilisateur trouvé avec l'email {Email}", email);

            return user;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération de l'utilisateur par email {Email}", email);
            throw;
        }
    }

    public async Task<Models.Entities.User?> GetById(int userId)
    {
        try
        {
            logger.LogInformation("Récupération de l'utilisateur par ID {UserId}", userId);
            var user = await userRepository.GetById(userId);

            if (user == null)
                logger.LogWarning("Aucun utilisateur trouvé avec l'ID {UserId}", userId);
            else
                logger.LogInformation("Utilisateur {UserId} trouvé", userId);

            return user;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération de l'utilisateur {UserId}", userId);
            throw;
        }
    }

    public async Task Save(Models.Entities.User newUser)
    {
        try
        {
            logger.LogInformation("Enregistrement d'un nouvel utilisateur avec l'email {Email}", newUser.Email);
            await userRepository.Save(newUser);
            logger.LogInformation("Utilisateur avec l'email {Email} enregistré avec succès", newUser.Email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'enregistrement de l'utilisateur {Email}", newUser.Email);
            throw;
        }
    }

    public async Task<UserDetailsDto> UpdateUserWithDetailsAsync(UserDetailsDto newUserDetailsDto, Models.Entities.User user)
    {
        using var transaction = await lodgerDbContext.Database.BeginTransactionAsync();

        try
        {

            var updateUser = mapper.Map(newUserDetailsDto, user);

            var address = await GetAddress(newUserDetailsDto.AddressName, newUserDetailsDto.PostalCode);
            
            updateUser.AddressId = address?.Id;
            updateUser.Id = updateUser.Id;

            updateUser = await userRepository.Save(updateUser);

            newUserDetailsDto = await GetUserWithDetailsAsync(updateUser.Id);

            await transaction.CommitAsync();

            return mapper.Map<UserDetailsDto>(updateUser);


        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "Erreur lors de l'enregistrement de l'utilisateur {Email}", newUserDetailsDto.Email);
            throw;
        }
    }

    private async Task<Address?> GetAddress(string? addressName, string? postalCode)
    {
        Address? address = null;
        if (addressName is not null && postalCode is not null)
        {
            address = await addressRepository.GetByAddress(addressName, postalCode) ?? await addressRepository.Save(new Address
            {
                AddressName = addressName,
                PostalCode = postalCode
            });
        }
        return address;
    }
    public async Task DeleteUserByIdAsync(int userId)
    {
        await userRepository.DeleteUserWithDependenciesAsync(userId);
    }



}
