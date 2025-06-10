import type {OptionLogement} from "~/models/entities/optionLogement";
import type {Adresse} from "~/models/entities/adresse";
import type {Photo} from "~/models/entities/photo";

export interface Logement {
    id: number;
    uuid: string;
    adresse: Adresse;
    superficie: number;
    nombreDePieces: number;
    meuble: boolean;
    loyer: number;
    charge: number;
    garantie: number;
    sdo: number;
    chambre: number;
    garage: number;
    etage: number | null;
    etageMax: number | null;
    description: string;
    type: string;
    classeEnergie: string;
    ges: string;
    position: [number, number];
    proprietaireId: number;
    locataireId: number;
    photos: Photo[];
    options: OptionLogement[];
}