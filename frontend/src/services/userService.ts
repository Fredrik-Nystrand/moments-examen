import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react"
import type { User, LoginInput } from "@/assets/types"
import type { RootState } from "@/store/store"

const BASE_URL = import.meta.env.VITE_API_URL

export const userApi = createApi({
  reducerPath: "userApi",
  baseQuery: fetchBaseQuery({
    baseUrl: BASE_URL,
    prepareHeaders: (headers, { getState }) => {
      const token: string | null = (getState() as RootState).user.user?.token
      headers.set("Authorization", `Bearer ${token}`)
    },
  }),
  tagTypes: ["User"],
  endpoints: (builder) => ({
    loginUser: builder.mutation<User, LoginInput>({
      query(data) {
        return {
          url: "users/login",
          method: "POST",
          body: data,
          headers: {},
        }
      },
    }),
    updateUser: builder.mutation<User, number>({
      query(id) {
        return {
          url: `Users/Get/${id}`,
          method: "GET",
          headers: {},
        }
      },
    }),
    getUser: builder.query<User, number | null>({
      query: (id) => `Users/Get/${id}`,
      providesTags: ["User"],
    }),
  }),
})

export const { useLoginUserMutation, useGetUserQuery, useUpdateUserMutation } =
  userApi
