import { PayloadAction, createSlice } from "@reduxjs/toolkit"
import type { User } from "@/assets/types"

interface IUserState {
  user: User
}

const initialState: IUserState = {
  user: {
    id: null,
    firstname: null,
    lastname: null,
    email: null,
    xp: null,
    currency: null,
    level: null,
    token: null,
    xpToday: null,
    xpThisMonth: null,
    xpLastMonth: null,
  },
}

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    loginUser: (state: IUserState, action: PayloadAction<User>) => {
      state.user = action.payload

      localStorage.setItem("token", JSON.stringify(action.payload.token))
    },
    logoutUser: (state: IUserState) => {
      state = { ...initialState }
      localStorage.removeItem("user")
    },
    updateUser: (state: IUserState, action: PayloadAction<User>) => {
      state.user = {
        id: action.payload.id,
        firstname: action.payload.firstname,
        lastname: action.payload.lastname,
        email: action.payload.email,
        xp: action.payload.xp,
        currency: action.payload.currency,
        level: action.payload.level,
        token: state.user.token,
        xpToday: action.payload.xpToday,
        xpThisMonth: action.payload.xpThisMonth,
        xpLastMonth: action.payload.xpLastMonth,
      }
    },
  },
})

export const { loginUser, logoutUser, updateUser } = userSlice.actions
