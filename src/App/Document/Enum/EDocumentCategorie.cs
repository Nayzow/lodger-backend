namespace LodgerBackend.Document.Enum
{
    public enum EDocumentCategorie
    {

        JUSTIFICATIF_DE_DOMICILE = 0,
        AVIS_IMPOSITION = 1,
        ATTESTATION_HEBERGEMENT = 2,
        FICHE_DE_PAIE = 3,
        ATTESTATION_DE_FOYER_FISCAL = 4,
        CERTIFICAT_DE_SCOLARITE = 5,
        CONTRAT_DE_TRAVAIL_GARANT = 6,
        JUSTIFICATIF_DE_DOMICILE_GARANT = 7,
        KIBS = 8,
        JUSTIFICATIF_SOLVABILITE = 9,
        PREUVE_ENGAGEMENT = 10,
        CERTIFICAT_GARANT_GARANTME = 11,
        CERTIFICAT_GARANT_CAUTIONEO = 12,
        ATTESTATION_GARANT_VISALE = 13,
        CERTIFICAT_GARANTIE_UNKLE = 14,
        PASSPORT_OU_CI = 15,
        INCONNU = 100
    }

    public static class DocumentCategorieExtensions
    {
        public static string GetName(this EDocumentCategorie documentCategorie)
        {
            return documentCategorie switch
            {
                EDocumentCategorie.JUSTIFICATIF_DE_DOMICILE => "Etat des lieux",
                EDocumentCategorie.AVIS_IMPOSITION => "Bail",
                EDocumentCategorie.ATTESTATION_HEBERGEMENT => "Quittance",
                EDocumentCategorie.FICHE_DE_PAIE => "Paiement",
                EDocumentCategorie.ATTESTATION_DE_FOYER_FISCAL => "Dossier",
                EDocumentCategorie.CERTIFICAT_DE_SCOLARITE => "Certificat de scolarité",
                EDocumentCategorie.CONTRAT_DE_TRAVAIL_GARANT => "Contrat de travail garant",
                EDocumentCategorie.JUSTIFICATIF_DE_DOMICILE_GARANT => "Justificatif de domicile garant",
                EDocumentCategorie.KIBS => "KIBS",
                EDocumentCategorie.JUSTIFICATIF_SOLVABILITE => "Justificatif de solvabilité",
                EDocumentCategorie.PREUVE_ENGAGEMENT => "Preuve d'engagement",
                EDocumentCategorie.CERTIFICAT_GARANT_GARANTME => "Certificat garant Garantme",
                EDocumentCategorie.CERTIFICAT_GARANT_CAUTIONEO => "Certificat garant Cautioneo",
                EDocumentCategorie.ATTESTATION_GARANT_VISALE => "Attestation garant Visale",
                EDocumentCategorie.CERTIFICAT_GARANTIE_UNKLE => "Certificat garantie Unkle",
                EDocumentCategorie.PASSPORT_OU_CI => "Passport ou carte d'identité (recto/verso)",
                EDocumentCategorie.INCONNU => "Inconnu",
                _ => "Inconnu"
            };
        }

        public static EDocumentCategorie FromName(string? name)
        {
            return name?.Trim().ToLower() switch
            {
                "etat des lieux" => EDocumentCategorie.JUSTIFICATIF_DE_DOMICILE,
                "bail" => EDocumentCategorie.AVIS_IMPOSITION,
                "quittance" => EDocumentCategorie.ATTESTATION_HEBERGEMENT,
                "paiement" => EDocumentCategorie.FICHE_DE_PAIE,
                "dossier" => EDocumentCategorie.ATTESTATION_DE_FOYER_FISCAL,
                "certificat de scolarité" => EDocumentCategorie.CERTIFICAT_DE_SCOLARITE,
                "contrat de travail garant" => EDocumentCategorie.CONTRAT_DE_TRAVAIL_GARANT,
                "justificatif de domicile garant" => EDocumentCategorie.JUSTIFICATIF_DE_DOMICILE_GARANT,
                "kibs" => EDocumentCategorie.KIBS,
                "justificatif de solvabilité" => EDocumentCategorie.JUSTIFICATIF_SOLVABILITE,
                "preuve d'engagement" => EDocumentCategorie.PREUVE_ENGAGEMENT,
                "certificat garant garantme" => EDocumentCategorie.CERTIFICAT_GARANT_GARANTME,
                "certificat garant cautioneo" => EDocumentCategorie.CERTIFICAT_GARANT_CAUTIONEO,
                "attestation garant visale" => EDocumentCategorie.ATTESTATION_GARANT_VISALE,
                "certificat garantie unkle" => EDocumentCategorie.CERTIFICAT_GARANTIE_UNKLE,
                "passport ou carte d'identité (recto/verso)" => EDocumentCategorie.PASSPORT_OU_CI,
                "inconnu" => EDocumentCategorie.INCONNU,
                _ => throw new ArgumentException($"Catégorie de document invalide : {name}")
            };
        }
    }
}
