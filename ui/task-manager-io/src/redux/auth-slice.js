import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
    name: 'auth',
    initialState: { 
        user: null, 
        accessToken: null, 
        refreshToken: null, 
        expiresAt: null 
    },
    reducers: {
        setCredentials: (state, action) => {
            const { user, accessToken, refreshToken, expiresAt } = action.payload;
            state.user = user;
            state.accessToken = accessToken;
            state.refreshToken = refreshToken;
            state.expiresAt = expiresAt;
        },
        logOut: (state, action) => {
            state.user = null;
            state.accessToken = null;
            state.refreshToken = null;
            state.expiresAt = null;
        }
    }
})

export const { setCredentials, logOut } = authSlice.actions

export default authSlice.reducer

export const selectCurrentUser = (state) => state.auth.user
export const selectCurrentToken = (state) => state.auth.accessToken
