import type {Logement} from "~/models/entities/logement";

export interface SearchResponse {
    content: Logement[];
    page: {
        size: number;
        totalElements: number;
        totalPages: number;
        number: number;
    }
}