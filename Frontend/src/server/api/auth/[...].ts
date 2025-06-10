import GoogleProvider from 'next-auth/providers/google'
import FacebookProvider from 'next-auth/providers/facebook'
import {NuxtAuthHandler} from '#auth'

export default NuxtAuthHandler({
    // A secret string you define, to ensure correct encryption
    secret: useRuntimeConfig().public.authSecret as string,
    providers: [
        // @ts-expect-error
        GoogleProvider.default({
            clientId: useRuntimeConfig().public.GoogleClientId as string,
            clientSecret: useRuntimeConfig().public.GoogleClientSecret as string,
            authorization: {
                params: {
                    redirect_uri: `http://localhost:3000/api/auth/callback/google`,
                },
            },
        }),
        // @ts-expect-error
        FacebookProvider.default({
            clientId: useRuntimeConfig().public.FacebookClientId as string,
            clientSecret: useRuntimeConfig().public.FacebookClientSecret as string,
            authorization: {
                params: {
                    redirect_uri: `http://localhost:3000/api/auth/callback/facebook`,
                },
            },
        }),
    ],
    callbacks: {
        async signIn({user, account, profile, email, credentials}) {
            return true
        },
        /* on redirect to another url */
        async redirect({url, baseUrl}) {
            return baseUrl
        },
        /* on session retrival */
        async session({session, user, token}) {
            return session
        },
        /* on JWT token creation or mutation */
        async jwt({token, user, account, profile, isNewUser}) {
            return token
        }
    },
})