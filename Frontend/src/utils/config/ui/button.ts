export const selectButtonUI = {
    rounded: 'rounded-xl',
    base: 'flex flex-row items-center gap-2',
    color: {
        white: {
            solid: 'shadow-sm ring-1 ring-inset ring-gray-300 text-secondaire bg-white hover:bg-principal hover:text-white disabled:bg-white aria-disabled:bg-white focus-visible:ring-2 focus-visible:ring-primary-500',
            outline: 'shadow-sm ring-1 ring-gray-300 text-secondaire bg-white hover:bg-principal hover:text-white disabled:bg-white aria-disabled:bg-white focus-visible:ring-2 focus-visible:ring-primary-500'
        }
    },
}

export const buttonUI = {
    rounded: 'rounded-xl',
    base: 'flex flex-row items-center gap-2 border-none',
    color: {
        white: {
            solid: 'shadow-sm text-white bg-principal hover:bg-principal/80 disabled:bg-white aria-disabled:bg-white ring-0'
        }
    }
}

export const iconButtonUI = {
    icon: {
        base: 'flex-shrink-0 tex-gray-400',
        loading: 'animate-spin',
        size: {
            sm: 'h-6 w-6',
            md: 'h-8 w-8',
            lg: 'h-10 w-10',
        }
    },
}
