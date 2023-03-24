import { apiSlice } from "./api-slice"

export const authApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        login: builder.mutation({
            query: credentials => ({
                url: '/api/login',
                method: 'POST',
                body: { ...credentials }
            })
        }),
        getCurrentUserProfile: builder.query({
            query: () => '/api/profiles/me',
            //keepUnusedDataFor: 10,
            providesTags: ['User']
        }),
        changePassword: builder.mutation({
            query: body => ({
                url: '/api/profiles/me/changePassword',
                method: 'PUT',
                body: {...body}
            }),
            providesTags: ['User']
        }),
        registerUser: builder.mutation({
            query: body => ({
                url: 'api/register',
                method: 'POST',
                body: {...body}
            })
        }),
        getUserProfiles: builder.query({
            query: () => '/api/profiles',
            providesTags: ['User']
        })
    })
})

export const {
    useLoginMutation,
    useGetCurrentUserProfileQuery,
    useChangePasswordMutation,
    useGetUserProfilesQuery,
    useRegisterUserMutation,
} = authApiSlice