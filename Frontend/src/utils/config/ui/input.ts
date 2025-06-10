export const advancedSearchInputUI = {
    base: 'shadow-md max-w-xs border border-gray-400',
    placeholder: 'flex-1 placeholder-secondaire font-jakarta',
    rounded: 'rounded-full',
    color: {
        white: {
            outline: 'shadow-lg bg-white text-secondaire ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-primary-500'
        },
    },
    icon: {
        color: 'text-secondaire',
        base: 'text-secondaire text-base',
    }
}

export const inputUI = {
    rounded: 'rounded-xl',
    placeholder: 'placeholder-secondaire',
    padding: {
        sm: 'px-4 py-2',
        xl: 'px-6 py-4',
    },
    color: {
        white: {
            outline: 'shadow-sm bg-white text-secondaire ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-primary-500'
        },
    },
    icon: {
        base: 'text-secondaire',
        trailing: {
            wrapper: 'absolute inset-y-0 end-0 flex items-center z-20',
            pointer: 'cursor-pointer',
        }
    }
}


export const inputUIPrincipal = {
    rounded: 'rounded-lg',
    placeholder: 'placeholder-secondaire',
    padding: {
        sm: 'px-4 py-2',
    },
    color: {
        white: {
            outline: 'shadow-sm bg-white ring-1 text-principal ring-inset ring-gray-300 focus:ring-2 focus:ring-primary-500'
        },
    },
    icon: {
        base: 'text-secondaire',
        trailing: {
            wrapper: 'z-50 absolute inset-y-0 end-0 flex items-center',
            pointer: 'cursor-pointer',
        }
    },
}