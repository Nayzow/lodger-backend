import type {DossierInfoDto} from "~/models/dto/dossier-info-dto";
import {FetchError} from "ofetch";
import {HttpMethod} from "~/models/enums/httpMethod";
import {DOSSIER_URL} from "~/utils/config/api/endpoints";

export const useDossier = () => {

    async function getDossier() {
        return await useApiFetch<DossierInfoDto>(DOSSIER_URL, {method: HttpMethod.GET});
    }

    async function updateDossier(dossier: DossierInfoDto) {
        return await $fetch<DossierInfoDto>(DOSSIER_URL, {
            method: HttpMethod.PUT,
            body: dossier,
        }).then((data) => {
            return {data, status: 'success', error: null};
        }).catch((error: FetchError<any>) => {
            return {data: null, status: 'error', error};
        });
    }

    return {getDossier, updateDossier};
};