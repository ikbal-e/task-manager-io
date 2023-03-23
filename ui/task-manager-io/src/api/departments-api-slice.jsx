import { apiSlice } from "./api-slice"

export const departmentsApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        getDepartments: builder.query({
            query: () => ({
                url: '/api/departments',
                method: 'GET',
            })
        }),
    })
})

export const {
    useLazyGetDepartmentsQuery
} = departmentsApiSlice