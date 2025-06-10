import type {Gender} from "~/models/enums/gender";
import type {Nationality} from "~/models/enums/nationality";

export interface ProfileInfoDto {
    nom?: string;
    prenom?: string;
    email?: string;
    telephone?: string;
    genre?: Gender;
    nationalite?: Nationality;
    dateNaissance?: string;
    adresse?: string;
    codePostal?: string;
    docIdentite?: string;
}