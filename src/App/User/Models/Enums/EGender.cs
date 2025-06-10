namespace LodgerBackend.App.User.Models.Enums
{
    public enum EGender
    {
        Homme = 0,
        Femme = 1,
        Autre = 2
    }
    public static class GenderExtensions
    {
        public static string GetName(this EGender gender)
        {
            return gender switch
            {
                EGender.Homme => "Homme",
                EGender.Femme => "Femme",
                EGender.Autre => "Autre",
                _ => "Inconnu"
            };
        }
        public static EGender FromName(string? name)
        {
            return name?.Trim().ToLower() switch
            {
                "homme" => EGender.Homme,
                "femme" => EGender.Femme,
                "autre" => EGender.Autre,
                _ => throw new ArgumentException($"Genre invalide : {name}")
            };
        }
    }

}
