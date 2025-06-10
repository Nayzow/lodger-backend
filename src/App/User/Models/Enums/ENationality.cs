
using System.Xml.Linq;

namespace LodgerBackend.App.User.Models.Enums
{
    public enum ENationality
    {
        FRANCAIS = 0,
        BELGE = 1,
        SUISSE = 2,
        CANADIEN = 3,
        AUTRE = 4
    }
    public static class NationalityExtensions
    {
        public static string GetName(this ENationality nationality)
        {
            return nationality switch
            {
                ENationality.FRANCAIS => "Français",
                ENationality.BELGE => "Belge",
                ENationality.SUISSE => "Suisse",
                ENationality.CANADIEN => "Canadien",
                ENationality.AUTRE => "Autre",
                _ => "Inconnu"
            };
        }

        public static ENationality FromName(string? nationalityName)
        {
            return nationalityName?.Trim().ToLower() switch
            {
                "français" => ENationality.FRANCAIS,
                "belge" => ENationality.BELGE,
                "suisse" => ENationality.SUISSE,
                "canadien" => ENationality.CANADIEN,
                "autre" => ENationality.AUTRE,
                _ => throw new ArgumentException($"Nationalité invalide : {nationalityName}")
            };
        }
    }


}
