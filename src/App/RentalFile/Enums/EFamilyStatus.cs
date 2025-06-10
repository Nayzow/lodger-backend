namespace LodgerBackend.RentalFile.Enums
{
    public enum EFamilyStatus
    {
        MARIE = 0,
        CELIBATAIRE = 1,
        DIVORCE = 2,
        SEPARE = 3,
        VEUF = 4,
        CONCUBIN = 5,
        PACSE = 6,
        AUTRE = 7
    }

    public static class FamilyStatusExtensions
    {
        public static string GetName(this EFamilyStatus statut)
        {
            return statut switch
            {
                EFamilyStatus.MARIE => "Marié",
                EFamilyStatus.CELIBATAIRE => "Célibataire",
                EFamilyStatus.DIVORCE => "Divorcé",
                EFamilyStatus.SEPARE => "Séparé",
                EFamilyStatus.VEUF => "Veuf",
                EFamilyStatus.CONCUBIN => "Concubin",
                EFamilyStatus.PACSE => "PACSé",
                EFamilyStatus.AUTRE => "Autre",
                _ => "Inconnu"
            };
        }
        public static EFamilyStatus FromName(string? name)
        {
            return name?.Trim().ToLower() switch
            {
                "marié" => EFamilyStatus.MARIE,
                "célibataire" => EFamilyStatus.CELIBATAIRE,
                "divorcé" => EFamilyStatus.DIVORCE,
                "séparé" => EFamilyStatus.SEPARE,
                "veuf" => EFamilyStatus.VEUF,
                "concubin" => EFamilyStatus.CONCUBIN,
                "pacsé" => EFamilyStatus.PACSE,
                "autre" => EFamilyStatus.AUTRE,
                _ => throw new ArgumentException($"Statut familial invalide : {name}")
            };
        }

    }
}
