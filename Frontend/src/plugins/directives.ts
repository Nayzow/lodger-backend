import { defineNuxtPlugin } from '#app';
import {minNumber} from "~/utils/directives/inputNumber";
import clickOutside from "~/utils/directives/clickOutside";

export default defineNuxtPlugin((nuxtApp) => {
    nuxtApp.vueApp.directive('min-number', minNumber);
    nuxtApp.vueApp.directive('click-outside', clickOutside)
});
