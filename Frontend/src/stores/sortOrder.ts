
export const useSortOrderStore = defineStore('sortOrder', {
    state: () => ({
        _sortOrder: ''
    }),
    getters: {
        sortOrder: (state) => state._sortOrder
    },
    actions: {
        setSortOrder(sortOrder: string) {
            this._sortOrder = sortOrder;
        },
        clear() {
            this._sortOrder = '';
        }
    },
    persist: true
});
