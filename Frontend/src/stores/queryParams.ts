import {defineStore} from 'pinia';
import {isValidParam, sanitizeInput} from "~/utils/helpers/validateUtils";
import type {LocationQuery} from "#vue-router";
import {serialize} from "~/utils/helpers/urlHelper";

export const useQueryParamsStore = defineStore('queryParams', {
    state: (): { _queryParams: LocationQuery } => ({
        _queryParams: {}
    }),
    getters: {
        queryParams: (state) => state._queryParams,
    },
    actions: {
        setQueryParams<T extends Record<string, any>>(params: T) {
            const filteredParams = Object.fromEntries(
                Object.entries(params).filter(([_, value]) => isValidParam(value))
            );
            this._queryParams = {...this._queryParams, ...filteredParams};
        },
        serializeQueryParams(queryParams?: LocationQuery) {
            const route = useRoute();
            const serializedStateParams = serialize(this._queryParams)
            const serializedQueryParams = serialize(queryParams || {});
            const urlSerializedQueryParams = serialize(route.query);

            return {
                ...urlSerializedQueryParams,
                ...serializedStateParams,
                ...serializedQueryParams,

            };
        },
        getSanitizedQueryParams() {
            const route = useRoute();
            const newQueryParams = route.query;
            const queryParams = {...newQueryParams, ...this._queryParams};
            return Object.fromEntries(
                Object.entries(queryParams).map(([key, value]) => [key, sanitizeInput(value)])
            );
        },
        clear() {
            this._queryParams = {};
        },
    },
});
