import { configureStore } from "@reduxjs/toolkit"
import { userSlice } from "./slices/usersSlice"
import { userApi } from "@/services/userService"
import { choresApi } from "@/services/choresServices"
import { completeChoreSlice } from "./slices/completeChoreSlice"

const store = configureStore({
  reducer: {
    [userApi.reducerPath]: userApi.reducer,
    [choresApi.reducerPath]: choresApi.reducer,
    user: userSlice.reducer,
    completeChore: completeChoreSlice.reducer,
  },
  middleware: (getDefaultMiddleWare) =>
    getDefaultMiddleWare()
      .concat(userApi.middleware)
      .concat(choresApi.middleware),
})

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch

export const selectUser = (state: RootState) => state.user

export default store
