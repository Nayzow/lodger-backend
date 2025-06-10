import HttpFactory from "~/repository/factory";
import {HttpMethod} from "~/models/enums/httpMethod";
import {SEARCH_LOGEMENT_URL} from "~/utils/config/api/endpoints";
import type {SearchResponse} from "~/models/payloads/searchResponse";
import type {PaginatedSearchRequest} from "~/models/payloads/searchRequest";

class LogementModule extends HttpFactory {
    async searchLogements(searchRequest: PaginatedSearchRequest): Promise<SearchResponse> {
        return await this.call<SearchResponse>(HttpMethod.POST, SEARCH_LOGEMENT_URL, searchRequest);
    }
}

export default LogementModule;