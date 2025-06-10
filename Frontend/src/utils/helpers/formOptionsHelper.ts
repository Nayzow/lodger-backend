import type { OptionConfig } from "~/models/types/formOptionConfig";
import type {Option} from "~/models/types/option";

type KeyValue<T> = { key: string; value: T };

function enumToKeyValueArray<T extends object>(enumObj: T): KeyValue<T[keyof T]>[] {
    return Object.entries(enumObj).map(([key, value]) => ({ key, value: value as T[keyof T] }));
}

export function generateOptions<T extends object>(items: T, config: OptionConfig<KeyValue<T[keyof T]>>) {
    const keyValueArray = enumToKeyValueArray(items);

    return keyValueArray.map((item) => {
        const option: Option = {
            value: config.value(item),
            label: config.label(item),
        };

        return option;
    });
}