import type {Directive, DirectiveBinding} from "vue";

export const minNumber: Directive = {
    mounted(el: HTMLInputElement, binding: DirectiveBinding) {
        const input = el.firstChild as HTMLInputElement;
        if (input.type === 'number') {
            input.setAttribute('min', binding.value);
        }
    },
    updated(el: HTMLInputElement, binding: DirectiveBinding) {
        const input = el.firstChild as HTMLInputElement;
        if (input.type === 'number') {
            input.setAttribute('min', binding.value);
        }
    }
}
