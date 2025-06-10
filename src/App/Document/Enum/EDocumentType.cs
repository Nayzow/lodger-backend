namespace LodgerBackend.App.Document.Enum
{
    public enum EDocumentType
    {
        ETAT_DES_LIEUX = 0,
        BAIL = 1,
        QUITTANCE = 2,
        PAIEMENT = 3,
        DOSSIER = 4
    }

    public static class DocumentTypeExtensions
    {
        public static string GetName(this EDocumentType documentType)
        {
            return documentType switch
            {
                EDocumentType.ETAT_DES_LIEUX => "Etat des lieux",
                EDocumentType.BAIL => "Bail",
                EDocumentType.QUITTANCE => "Quittance",
                EDocumentType.PAIEMENT => "Paiement",
                EDocumentType.DOSSIER => "Dossier",
                _ => "Inconnu"
            };
        }

        public static EDocumentType FromName(string? name)
        {
            return name?.Trim().ToLower() switch
            {
                "etat des lieux" => EDocumentType.ETAT_DES_LIEUX,
                "bail" => EDocumentType.BAIL,
                "quittance" => EDocumentType.QUITTANCE,
                "paiement" => EDocumentType.PAIEMENT,
                "dossier" => EDocumentType.DOSSIER,
                _ => throw new ArgumentException($"Type de document invalide : {name}")
            };
        }
    }
}
