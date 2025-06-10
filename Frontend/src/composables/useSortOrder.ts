import {useSortOrderStore} from "~/stores/sortOrder";
import {useLogementsStore} from "~/stores/logements";

export function useSortOrder() {
    const route = useRoute();
    const {searchLogements} = useLogementsStore();

    const sortOrderStore = useSortOrderStore();
    const {sortOrder} = storeToRefs(sortOrderStore);

    const queryParamStore = useQueryParamsStore();
    const {queryParams} = storeToRefs(queryParamStore);

    const state = ref(sortOrder?.value ?? '');

    let previousRoute = '';

    watchEffect(() => {
        previousRoute = route.fullPath;
    });

    watch(sortOrder, (value) => {
        state.value = value;
    });

    watch(state, async () => {
        sortOrderStore.setSortOrder(state.value ?? '');
        queryParamStore.setQueryParams({ordre: sortOrder.value});
        await navigateTo({path: '/logements', query: queryParamStore.serializeQueryParams(queryParams.value)});
        if (previousRoute.includes('/logements')) {
            await searchLogements({page: 1, size: 10});
        }
    });

    function handleReset() {
        sortOrderStore.clear();
    }

    return {
        state,
        handleReset
    };
}