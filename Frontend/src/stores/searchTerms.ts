import {defineStore} from "pinia";
import {isValidParam, sanitizeInput} from "~/utils/helpers/validateUtils";
import type {SearchRequest} from "~/models/payloads/searchRequest";

const initialSearchTerms: SearchRequest = {
    type: 'Appartement',
    localisation: '',
    prixMin: 400,
    prixMax: 800,
}
export const useSearchTermsStore = defineStore('searchTerms', {
    state: (): { _searchTerms: SearchRequest } => ({
        _searchTerms: initialSearchTerms
    }),
    getters: {
        searchTerms: (state): SearchRequest => state._searchTerms
    },
    actions: {
        setSearchTerms(searchTerms: SearchRequest) {
            const filteredTerms = Object.fromEntries(
                Object.entries(searchTerms)
                    .filter(([_, value]) => isValidParam(sanitizeInput(value)))
            );
            this._searchTerms = {...this._searchTerms, ...filteredTerms};
        },
        clear() {
            this._searchTerms = initialSearchTerms;
        }
    },
    persist: true
});
