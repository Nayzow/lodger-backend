
export function addAuthorizationHeader(options: any): any {
    const cookie = useCookie('accessToken');
    if (!cookie.value) {
        return options;
    }

    return {
        ...options,
        headers: {
            ...options.headers,
            Authorization: `Bearer ${cookie.value}`,
        },
    };
}