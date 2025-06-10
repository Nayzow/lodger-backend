import type {SearchRequest} from "~/models/payloads/searchRequest";
import {addToArray} from "~/utils/helpers/formArray";

export function useSearchForm<T extends Record<string, any>>(initialState: () => T) {
    const route = useRoute();

    const {searchLogements} = useLogementsStore();
    const searchTermsStore = useSearchTermsStore();
    const sortOrderStore = useSortOrderStore();
    const queryParamStore = useQueryParamsStore();

    const {searchTerms} = storeToRefs(searchTermsStore);
    const {queryParams} = storeToRefs(queryParamStore);

    const { triggerCloseAllModals } = useModalCloser();

    const state = reactive<T>({...initialState()});
    const newMotCle = ref('');

    let previousRoute = '';

    watchEffect(() => {
        previousRoute = route.fullPath;
    });

    function addMotCle() {
        if ('motsCles' in state) {
            addToArray(newMotCle.value, state.motsCles);
            newMotCle.value = '';
        }
    }

    function savePreviousSearch(search: string) {
        if (!search) {
            return
        }
        let searches = JSON.parse(localStorage.getItem('previousSearches') || '[]')
        searches = searches.filter((s: string) => s !== search)
        searches.unshift(search)
        if (searches.length > 5) {
            searches.pop()
        }
        localStorage.setItem('previousSearches', JSON.stringify(searches))
    }

    async function onSubmit() {
        try {
            triggerCloseAllModals();

            searchTermsStore.clear();
            queryParamStore.clear();
            sortOrderStore.clear();

            searchTermsStore.setSearchTerms(state as SearchRequest);
            queryParamStore.setQueryParams({...searchTerms.value, page: 1});

            if ('localisation' in state) {
                savePreviousSearch(state.localisation)
            }

            await navigateTo({path: '/logements', query: queryParamStore.serializeQueryParams(queryParams.value)});
            if (previousRoute.includes('/logements')) {
                await searchLogements({page: 1});
            }
        } catch (error) {
            console.error('Error in onSubmit:', error);
            // Gérer l'erreur ici (par exemple, afficher un message à l'utilisateur)
        }
    }

    function handleReset() {
        searchTermsStore.clear();
        queryParamStore.clear();
        Object.assign(state, initialState());
    }

    return {
        state,
        onSubmit,
        handleReset,
        newMotCle,
        addMotCle,
    };
}
