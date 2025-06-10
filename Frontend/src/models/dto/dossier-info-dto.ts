export interface DossierInfoDto {
  statutProfessionnel: string;
  revenuMensuel: string;
  garant: string;
  revenuGarant: string;
  statusFamiliale: string;
  nbColocataires: string;
  animaux: string;
  fumeur: string;
  commentaire: string;
  files: Array<{ name: string; type: string; data: File }>;
}
