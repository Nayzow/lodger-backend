using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace LodgerBackend.User.Models.Dtos;
public class UserDetailsDto
{
    [JsonPropertyName("nom")]
    public required string Name { get; set; }

    [JsonPropertyName("prenom")]
    public required string FirstName { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("telephone")]
    [FromForm(Name = "telephone")]
    public required string Phone { get; set; }

    [JsonPropertyName("genre")]
    public required string GenderName { get; set; }

    [JsonPropertyName("nationalite")]
    public required string NationalityName { get; set; }

    [JsonPropertyName("dateNaissance")]
    public DateTime? Birthday { get; set; }

    [JsonPropertyName("adresse")]
    public required string AddressName { get; set; }

    [JsonPropertyName("codePostal")]
    public required string PostalCode { get; set; }

    [JsonPropertyName("docIdentite")]
    public IFormFile? IdentityFile { get; set; }

}


