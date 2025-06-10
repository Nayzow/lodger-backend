import type {FileInfo} from "~/models/dto/file-info-dto";

export interface UpdateDossierDto {
    statutProfessionnel: string;
    revenuMensuel: string;
    garant: string;
    revenuGarant: string;
    statusFamiliale: string;
    nbColocataires: string;
    animaux: string;
    fumeur: string;
    commentaire: string;
    files: FileInfo[];
}