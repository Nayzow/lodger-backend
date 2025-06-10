// composables/useCarouselLogic.ts
import { ref, computed } from 'vue';

export default function useCarouselLogic(totalItems: number) {
    const isNavigating = ref(false);
    const currentPage = ref(0);

    const handleImageClick = (navigateToDetails: () => void) => {
        if (!isNavigating.value) {
            navigateToDetails();
        }
    };

    const handleCarouselNavigation = () => {
        isNavigating.value = true;
        console.log('Navigating to page', currentPage.value);
        setTimeout(() => {
            isNavigating.value = false;
        }, 100);
    };

    const prevClick = (onClick: () => void) => {
        if (!isNavigating.value) {
            currentPage.value = Math.max(0, currentPage.value - 1);
            onClick();
        }
    };

    const nextClick = (onClick: () => void) => {
        if (!isNavigating.value) {
            currentPage.value = Math.min(totalItems - 1, currentPage.value + 1);
            onClick();
        }
    };

    const visibleIndicators = computed(() => {
        const maxIndicators = 5;
        const total = totalItems;
        const current = currentPage.value;

        if (total <= maxIndicators) return Array.from({length: total}, (_, i) => i);

        let start = Math.max(0, Math.min(current - 2, total - maxIndicators));
        let end = Math.min(start + maxIndicators, total);

        if (end - start < maxIndicators) {
            start = Math.max(0, end - maxIndicators);
        }

        return Array.from({length: end - start}, (_, i) => i + start);
    });

    return {
        isNavigating,
        currentPage,
        handleImageClick,
        handleCarouselNavigation,
        prevClick,
        nextClick,
        visibleIndicators
    };
}