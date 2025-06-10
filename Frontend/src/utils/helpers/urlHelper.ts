import {sanitizeInput} from "~/utils/helpers/validateUtils";

export function serialize(params: Record<string, any>) {
    return Object.entries(params).reduce((acc, [key, value]) => {
        const sanitizedValue = sanitizeInput(value);
        acc[key] = Array.isArray(sanitizedValue)
            ? sanitizedValue.map(item => encodeURIComponent(item)).join(',')
            : encodeURIComponent(sanitizedValue);
        return acc;
    }, {} as Record<string, string>);
}