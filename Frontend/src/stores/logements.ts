import type {SearchResponse} from "~/models/payloads/searchResponse";
import type {Logement} from "~/models/entities/logement";

export const useLogementsStore = defineStore('logements', {
    state: (): { _logements: SearchResponse | null, _logement: Logement | null } => ({
        _logements: null,
        _logement: null,
    }),
    getters: {
        logements: (state): SearchResponse | null => state._logements,
        logement: (state): Logement | null => state._logement,
    },
    actions: {
        async getLogements() {
            const {getLogements} = useLogement();
            const {data} = await getLogements();

            if (data.value) {
                this.setLogements(data.value);
            }
        },
        async searchLogements({page, size}: { page?: number, size?: number }) {
            const {searchLogements} = useLogement();
            const searchResponse = await searchLogements({page, size});

            this.setLogements(searchResponse);
        },
        async getLogementById(id: string) {
            const {getLogementById} = useLogement();
            const {data} = await getLogementById(id);

            if (data.value) {
                this.setLogement(data.value);
            }
        },
        setLogements(logements: SearchResponse | null) {
            this._logements = logements;
        },
        setLogement(logement: Logement | null) {
            this._logement = logement
        }
    },
});
