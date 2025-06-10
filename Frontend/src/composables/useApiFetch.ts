import type {UseFetchOptions} from "#app";

export const useApiFetch = async <T>(url: string, options?: UseFetchOptions<T>) => {
    const {$backendURL} = useNuxtApp()
    const {data, status, error } = await useFetch(url, {
        baseURL: $backendURL as string,
        headers: {
            "Content-Type": "application/json",
        },
        ...options,
    });

    return { data, status, error };
}