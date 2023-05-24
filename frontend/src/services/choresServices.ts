import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react"
import type {
  ComlpetedChoreRequest,
  CompletedChore,
  Chore,
  User,
} from "@/assets/types"
import type { RootState } from "@/store/store"

const BASE_URL = import.meta.env.VITE_API_URL

export const choresApi = createApi({
  reducerPath: "choresApi",
  baseQuery: fetchBaseQuery({
    baseUrl: BASE_URL,
    prepareHeaders: (headers, { getState }) => {
      const user: User = (getState() as RootState).user.user
      headers.set("Authorization", `Bearer ${user.token}`)
    },
  }),
  tagTypes: ["CompletedChores"],
  endpoints: (builder) => ({
    completeChore: builder.mutation<CompletedChore, ComlpetedChoreRequest>({
      query(data) {
        return {
          url: "CompletedChores/Create",
          method: "POST",
          body: data,
        }
      },
      invalidatesTags: ["CompletedChores"],
    }),
    getCompletedChores: builder.query<
      CompletedChore[],
      number | null | undefined
    >({
      query: (userId) => `CompletedChores/GetAllForUser/${userId}`,
      providesTags: ["CompletedChores"],
    }),
    getChores: builder.query<Chore[], undefined>({
      query: () => `Chores`,
    }),
  }),
})

export const {
  useCompleteChoreMutation,
  useGetCompletedChoresQuery,
  useGetChoresQuery,
} = choresApi
