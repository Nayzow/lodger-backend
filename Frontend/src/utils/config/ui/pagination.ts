export const paginationUI = {
    wrapper: 'flex items-center justify-center space-x-px',
    base: 'h-5 w-5 p-4 flex items-center justify-center shadow-none ring-0 text-sm font-medium hover:bg-primaire',
    rounded: 'first:rounded-full rounded-full last:rounded-full',
    default: {
        size: 'sm',
        activeButton: {
            color: 'white'
        },
        inactiveButton: {
            color: 'primary'
        },
        prevButton: {
            color: 'transparent',
            class: 'rtl:[&_span:first-child]:rotate-180',
            icon: 'i-heroicons-chevron-left-20-solid'
        },
        nextButton: {
            color: 'transparent',
            class: 'rtl:[&_span:last-child]:rotate-180',
            icon: 'i-heroicons-chevron-right-20-solid'
        }
    }
}
