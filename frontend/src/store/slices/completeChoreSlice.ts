import { PayloadAction, createSlice } from "@reduxjs/toolkit"
import type { ComlpetedChoreRequest } from "@/assets/types"

interface ICompleteChoreState {
  completeChore: ComlpetedChoreRequest
}

const initialState: ICompleteChoreState = {
  completeChore: {
    choreId: null,
    userId: null,
    amount: null,
    dateCompleted: null,
  },
}

export const completeChoreSlice = createSlice({
  name: "completeChore",
  initialState,
  reducers: {
    updateInfo: (
      state: ICompleteChoreState,
      action: PayloadAction<ComlpetedChoreRequest>
    ) => {
      state.completeChore = action.payload
    },
  },
})

export const { updateInfo } = completeChoreSlice.actions
