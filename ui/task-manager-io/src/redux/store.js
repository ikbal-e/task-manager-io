import { configureStore } from '@reduxjs/toolkit'
import counterReducer from './counter-slice'
import authReducer from './auth-slice'
import { setupListeners } from '@reduxjs/toolkit/dist/query'
import { apiSlice } from '../api/api-slice'

export const store = configureStore({
  reducer: {
      [apiSlice.reducerPath]: apiSlice.reducer,
      counter: counterReducer,
      auth: authReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware),
})

setupListeners(store.dispatch)