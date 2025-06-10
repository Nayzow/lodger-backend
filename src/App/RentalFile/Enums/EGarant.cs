namespace LodgerBackend.RentalFile.Enums
{
    public enum EGarant
    {
        Non = 0,
        Tiers = 1,
        GarantieVisale = 2,
        GarantieCautioneo = 3,
        GarantieGarantme = 4,
        GarantieUnkle = 5,
        PersonneMorale = 6,
        Famille = 7,

    }

    public static class GarantExtensions
    {
        public static string GetName(this EGarant garant)
        {
            return garant switch
            {
                EGarant.Famille => "Famille",
                EGarant.Non => "Non",
                EGarant.Tiers => "Tiers",
                EGarant.GarantieVisale => "Garantie visale",
                EGarant.GarantieCautioneo => "Garantie Cautioneo",
                EGarant.GarantieGarantme => "Garantie Garantme",
                EGarant.GarantieUnkle => "Garantie Unkle",
                EGarant.PersonneMorale => "Personne morale",
                _ => "Non"
            };
        }
        public static EGarant FromName(string? name)
        {
            return name?.Trim().ToLower() switch
            {
                "famille" => EGarant.Famille,
                "non" => EGarant.Non,
                "tiers" => EGarant.Tiers,
                "garantie visale" => EGarant.GarantieVisale,
                "garantie cautioneo" => EGarant.GarantieCautioneo,
                "garantie garantme" => EGarant.GarantieGarantme,
                "garantie unkle" => EGarant.GarantieUnkle,
                "personne morale" => EGarant.PersonneMorale,
                _ => throw new ArgumentException($"Type de garant invalide : {name}")
            };
        }

    }
}
