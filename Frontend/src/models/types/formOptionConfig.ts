export type OptionConfig<T> = {
    value: (item: T) => string;
    label: (item: T) => string;
};
