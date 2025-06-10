using System.ComponentModel;
using System.Text.Json.Serialization;
using LodgerBackend.App.Document.Models;
using LodgerBackend.App.User.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LodgerBackend.App.User.Models.Dtos;
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


