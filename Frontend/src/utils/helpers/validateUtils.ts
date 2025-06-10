export function isValidParam(value: any): boolean {
    if (Array.isArray(value)) {
        return value.length > 0 && value.every(item => typeof item === 'string' && /^[a-zA-Z0-9_\-, ]+$/.test(item));
    }

    if (typeof value === 'number') {
        return value >= 0;
    }

    return value !== "" && value !== null && value !== undefined;
}

export function sanitizeInput(value: any): any {
    if (Array.isArray(value)) {
        return value.map(item => sanitizeInput(item));
    }
    if (typeof value === 'string') {
        return value.replace(/</g, "&lt;").replace(/>/g, "&gt;"); // Échapper les caractères spéciaux
    }
    return value;
}

export function passwordStrengthCheck(password: string): boolean {
    const upperCasePattern = /(?=.*[A-Z])/;
    const lowerCasePattern = /(?=.*[a-z])/;
    const digitPattern = /(?=.*\d)/;
    const specialCharPattern = /(?=.*[^a-zA-Z0-9])/;

    return upperCasePattern.test(password) &&
    lowerCasePattern.test(password) &&
    digitPattern.test(password) &&
    specialCharPattern.test(password);
}