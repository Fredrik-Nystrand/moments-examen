export type Chore = {
  id: number
  name: string
  description: string
  baseXp: SVGAnimatedNumberList
  isTimebased: boolean
  choreCategoryId: number
  choreCategoryName: string
}

export type User = {
  id: number | null
  firstname: string | null
  lastname: string | null
  email: string | null
  xp: number | null
  currency: number | null
  level: number | null
  token: string | null
  xpToday: number | null
  xpThisMonth: number | null
  xpLastMonth: number | null
}

export type LoginInput = {
  email: string
  password: string
}

export type CompletedChore = {
  id: number
  completedXp: number
  dateCompleted: string
  choreId: number
  choreName: string
  userId: number
  userName: string
  amount: string
  isTimebased: boolean
}

export type ComlpetedChoreRequest = {
  dateCompleted: string | null
  choreId: number | null
  userId: number | null
  amount: string | null
}

export type MathOperators = {
  operation: "Add" | "Sub"
}
