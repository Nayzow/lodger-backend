/** @type {import('tailwindcss').Config} */
export default {
    content: [
        "./src/components/**/*.{js,vue,ts}",
        "./src/layouts/**/*.vue",
        "./src/views/**/*.vue",
        "./src/plugins/**/*.{js,ts}",
        "./nuxt.config.{js,ts}",
        "./node_modules/flowbite/**/*.{js,ts}"
    ],
    theme: {
        extend: {
            screens: {
                '3xl': '1600px',
                '4xl': '1920px',
                '5xl': '2560px',
            },
            maxWidth: {
                '8xl': '1920px',
                '9xl': '2560px',
            },
            fontFamily: {
                ninetea: ['Ninetea', 'sans-serif'],
                jakarta: ['Jakarta', 'sans-serif'],
                albert: ['Albert', 'sans-serif'],
            },
            colors: {
                principal: '#02DB82',
                secondaire: '#02504D',
            },
            backgroundColor: {
                principal: '#02DB82',
                secondaire: '#02504D',
            },
            backgroundImage: {
                'hero-background': "url('/images/hero-background.jpg')",
            },
            border: {
                principal: '#02DB82',
                secondaire: '#02504D',
            }
        }
    },
}

