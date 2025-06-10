export default ({
    ssr: true,
    srcDir: 'src',

    modules: [
        '@nuxt/ui',
        '@nuxt/image',
        '@nuxtjs/leaflet',
        '@nuxtjs/tailwindcss',
        '@pinia/nuxt',
        'pinia-plugin-persistedstate/nuxt',
        '@nuxt/fonts',
        '@nuxt/icon',
        'nuxt-highcharts',
        '@nuxtjs/device',
        '@sidebase/nuxt-auth'
    ],

    buildModules: [
        '@nuxtjs/tailwindcss',
        '@pinia/nuxt'
    ],

    plugins: [
        '~/plugins/api.ts',
        '~/plugins/directives.ts',
        '~/plugins/urlProvider.ts'
    ],

    auth: {
        baseURL: "http://localhost:3000/api/auth",
        provider: {
            type: 'authjs',
            token: '',
            addDefaultCallbackUrl: false,
            globalAppMiddleware: false,
        },
        addDefaultCallbackUrl: false,
    },

    components: [
        {
            path: '~/components',
            pathPrefix: false
        }
    ],

    icon: {
        provider: 'iconify', // <-- this
        customCollections: [
            {
                prefix: 'custom',
                dir: './src/assets/icons',
                normalizeIconName: false, // <-- this
            }
        ]
    },

    css: [
        '@/assets/css/main.css'
    ],

    ui: {
        primary: '#02504D',
        emerald: '#02DB82',
        notifications: {
            // Show toasts at the top right of the screen
            position: 'top-0 bottom-[unset]'
        }
    },

    colorMode: {
        preference: 'light'
    },

    tailwindcss: {
        cssPath: '~/assets/css/tailwind.css',
        configPath: 'tailwind.config.js',
        exposeConfig: false
    },

    build: {
        extractCSS: true,
        optimization: {
            splitChunks: {
                layouts: true,
                pages: true,
                commons: true
            }
        },
        filenames: {
            app: ({isDev}: { isDev: boolean }) => isDev ? '[name].js' : '[contenthash].js',
            chunk: ({isDev}: { isDev: boolean }) => isDev ? '[name].js' : '[contenthash].js',
            css: ({isDev}: { isDev: boolean }) => isDev ? '[name].css' : '[contenthash].css',
            img: ({isDev}: { isDev: boolean }) => isDev ? '[path][name].[ext]' : '[contenthash:7].[ext]',
            font: ({isDev}: { isDev: boolean }) => isDev ? '[path][name].[ext]' : '[contenthash:7].[ext]',
            video: ({isDev}: { isDev: boolean }) => isDev ? '[path][name].[ext]' : '[contenthash:7].[ext]'
        }
    },

    typescript: {
        shim: false
    },

    devtools: {
        enabled: true
    },

    runtimeConfig: {
        public: {
            API_URL: process.env.API_URL ?? '/api/lodger',
            API_URL_SERVER: process.env.API_URL_SERVER ?? 'http://springboot-betalodger-service:1145/api/lodger',
            GoogleClientId: process.env.GOOGLE_CLIENT_ID,
            GoogleClientSecret: process.env.GOOGLE_CLIENT_SECRET,
            FacebookClientId: process.env.FACEBOOK_CLIENT_ID,
            FacebookClientSecret: process.env.FACEBOOK_CLIENT_SECRET,
            authSecret: process.env.AUTH_SECRET ?? 'auth-secret'
        }
    },

    analyze: process.env.ANALYZE || false,
    compatibilityDZate: '2024-10-21',
    compatibilityDate: '2025-01-12'
});