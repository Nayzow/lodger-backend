import {LOGEMENT_UUID_URL, SEARCH_LOGEMENT_URL} from "~/utils/config/api/endpoints";
import {HttpMethod} from "~/models/enums/httpMethod";
import type {PaginatedSearchRequest} from "~/models/payloads/searchRequest";
import type {SearchResponse} from "~/models/payloads/searchResponse";
import type {Logement} from "~/models/entities/logement";
import {useApiFetch} from "~/composables/useApiFetch";
import {FetchError} from "ofetch";
import type {ApiInstance} from "~/plugins/api";

export const useLogement = () => {
    const {$api} = useNuxtApp();
    const logementModule = ($api as ApiInstance).logements;

    const {getSanitizedQueryParams} = useQueryParamsStore();

    async function getLogements(): Promise<{
        data: Ref<SearchResponse | null>,
        status: Ref<string>,
        error: Ref<FetchError<any> | null>
    }> {
        const searchTerms = getSanitizedQueryParams();

        const {data, status, error} = await useApiFetch<SearchResponse>(SEARCH_LOGEMENT_URL, {
            method: "POST",
            body: {
                page: 1,
                size: 5,
                ...searchTerms,
            }
        });

        return {data, status, error};
    }

    async function getLogementById(id: string): Promise<{
        data: Ref<Logement | null>,
        status: Ref<string>,
        error: Ref<FetchError<any> | null>
    }> {
        const url = `${LOGEMENT_UUID_URL}/${id}`;
        const {data, status, error} = await useApiFetch<Logement>(url, {method: HttpMethod.GET});

        return {data, status, error};
    }

    async function searchLogements({page = 1, size = 5}: { page?: number, size?: number }): Promise<SearchResponse> {
        const searchTerms = getSanitizedQueryParams();
        return await logementModule.searchLogements({
            ...searchTerms as PaginatedSearchRequest,
            page,
            size
        });
    }

    return {getLogements, getLogementById, searchLogements};
};
