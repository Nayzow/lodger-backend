import type {Directive, DirectiveBinding} from "vue";

export const clickOutside: Directive = {
  mounted(el, binding: DirectiveBinding) {
    console.log("el", el)
    el.clickOutsideEvent = (event: Event) => {
      console.log('clickOutsideEvent')
      if (!(el === event.target || el.contains(event.target as Node))) {
        binding.value(event)
      }
    }
    console.log('clickOutsideEvent')
    document.addEventListener('click', el.clickOutsideEvent)
  },
  unmounted(el) {
    document.removeEventListener('click', el.clickOutsideEvent)
  },
}

export default clickOutside