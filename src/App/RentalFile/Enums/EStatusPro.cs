namespace LodgerBackend.RentalFile.Enums
{
    public enum EStatusPro
    {
        CDI = 0,
        CDI_PERIODE_ESSAI = 1,
        CDD = 2,
        INTERIM = 3,
        INDEPENDANT = 4,
        PROFESSION_LIBERALE = 5,
        FONCTIONNAIRE = 6,
        CHOMAGE = 7,
        ETUDIANT = 8,
        ALTERNANT = 9,
        STAGE = 10,
        RETRAITE = 11,
        SANS_EMPLOI = 12
    }

    public static class StatusProExtensions
    {
        public static string GetName(this EStatusPro statut)
        {
            return statut switch
            {
                EStatusPro.CDI => "CDI",
                EStatusPro.CDI_PERIODE_ESSAI => "CDI (période d’essai)",
                EStatusPro.CDD => "CDD",
                EStatusPro.INTERIM => "Interim",
                EStatusPro.INDEPENDANT => "Indépendant",
                EStatusPro.PROFESSION_LIBERALE => "Profession libérale",
                EStatusPro.FONCTIONNAIRE => "Fonctionaire",
                EStatusPro.CHOMAGE => "Chômage",
                EStatusPro.ETUDIANT => "Etudiant",
                EStatusPro.ALTERNANT => "Alternant",
                EStatusPro.STAGE => "Stage",
                EStatusPro.RETRAITE => "Retraité",
                EStatusPro.SANS_EMPLOI => "Sans emploi",
                _ => "Inconnu"
            };
        }
        public static EStatusPro FromName(string? name)
        {
            return name?.Trim().ToLower() switch
            {
                "cdi" => EStatusPro.CDI,
                "cdi (période d’essai)" => EStatusPro.CDI_PERIODE_ESSAI,
                "cdd" => EStatusPro.CDD,
                "interim" => EStatusPro.INTERIM,
                "indépendant" => EStatusPro.INDEPENDANT,
                "profession libérale" => EStatusPro.PROFESSION_LIBERALE,
                "fonctionaire" => EStatusPro.FONCTIONNAIRE,
                "chômage" => EStatusPro.CHOMAGE,
                "etudiant" => EStatusPro.ETUDIANT,
                "alternant" => EStatusPro.ALTERNANT,
                "stage" => EStatusPro.STAGE,
                "retraité" => EStatusPro.RETRAITE,
                "sans emploi" => EStatusPro.SANS_EMPLOI,
                _ => throw new ArgumentException($"Statut professionnel invalide : {name}")
            };
        }

    }
}
