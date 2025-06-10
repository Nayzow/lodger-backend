export function addToArray(value: string, array: string[], maxLength: number = 4) {
    if (!value || array?.includes(value) || array?.length >= maxLength) {
        return;
    }
    array.push(value);
}

export function removeFromArray(value: string, array?: string[]) {
    if (!value || !array) {
        return;
    }
    const index = array.indexOf(value);
    if (index !== -1) {
        array.splice(index, 1);
    }
}

export function toggleValue({value, array, maxLength = 4}: { value: string, array: string[], maxLength?: number }) {
    if (array.includes(value)) {
        removeFromArray(value, array);
    } else {
        addToArray(value, array, maxLength);
    }
}