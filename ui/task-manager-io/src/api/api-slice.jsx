import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { useNavigate } from 'react-router-dom';
import { logOut, setCredentials } from '../redux/auth-slice';

const baseQuery = fetchBaseQuery({
    baseUrl: 'https://localhost:7144', //TODO: get from env
    credentials: 'include',
    prepareHeaders: (headers, { getState }) => {
        const token = getState().auth.accessToken;
        if (token) {
            headers.set("authorization", `Bearer ${token}`)
        }
        return headers
    }
})

const baseQueryWithReauth = async (args, api, extraOptions) => {
    let result = await baseQuery(args, api, extraOptions)
    if (result.error && result.error.status === 401) {

        const refreshToken = api.getState().auth.refreshToken;
        const refreshResult = await baseQuery({ url: '/api/login/refresh', method: 'POST', body: { refreshToken } }, api, extraOptions);
        console.log(refreshResult);
        if (refreshResult.data) {
            api.dispatch(setCredentials(refreshResult.data));
            localStorage.setItem('token', refreshResult.accessToken);
            localStorage.setItem('refreshToken', refreshResult.refreshToken);
            localStorage.setItem('expiresAt', refreshResult.expiresAt);
            localStorage.setItem('role', refreshResult.role);

            result = await baseQuery(args, api, extraOptions);
        } else {
            api.dispatch(logOut());
            localStorage.clear('token');
            localStorage.clear('refreshToken');
            localStorage.clear('expiresAt');
            localStorage.clear('role');
            window.location.href = "/login";
        }
    }
    return result;
}

export const apiSlice = createApi({
    baseQuery: baseQueryWithReauth,
    endpoints: builder => ({}),
    tagTypes: ['User']
})