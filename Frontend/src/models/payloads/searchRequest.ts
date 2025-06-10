import type {PaginatedRequest} from "~/models/payloads/paginatedRequest";
import type {PropertyType} from "~/models/types/propertyType";

export interface PaginatedSearchRequest extends PaginatedRequest {
    type: string;
    localisation?: string;
    prixMin: number;
    prixMax: number;
    surfaceHabitableMin?: number;
    surfaceHabitableMax?: number;
    surfaceTerrainMin?: number;
    surfaceTerrainMax?: number;
    nombreDePieces?: string;
    nombreDeChambres?: string;
    caracteristiques?: string[];
    motsCles?: string[];
    dpo?: string;
    chauffage?: string;
    strict?: boolean;
}

export type SearchRequest = Omit<PaginatedSearchRequest, 'page' | 'size'>