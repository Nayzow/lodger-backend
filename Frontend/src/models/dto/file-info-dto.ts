import type {TypeFichier} from "~/models/enums/typeFichier";

export interface FileInfo {
    name: string;
    type: TypeFichier;
    data: File;
}