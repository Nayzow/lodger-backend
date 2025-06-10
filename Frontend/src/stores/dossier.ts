import type {DossierInfoDto} from "~/models/dto/dossier-info-dto";
import {useDossier} from "~/composables/useDossier";

export const useDossierStore = defineStore('dossier', {
    state: (): { dossier: DossierInfoDto | null } => ({
        dossier: null,
    }),
    actions: {
        async getDossier() {
            const {data} = await useDossier().getDossier();
            this.setDossier(data.value);
        },
        async updateDossier(dossier: DossierInfoDto) {
            const {data} = await useDossier().updateDossier(dossier);
            this.setDossier(data);
        },
        setDossier(dossier: DossierInfoDto | null) {
            this.dossier = dossier;
        },
    },
    persist: true,
});