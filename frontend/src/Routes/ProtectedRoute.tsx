import { ReactElement } from "react"
import { useAppSelector } from "@/hooks/reduxHooks"
import { Navigate, useLocation } from "react-router-dom"

type Props = {
  children: ReactElement
}

const ProtectedRoute = (props: Props) => {
  const token: string | null = useAppSelector((state) => state.user.user?.token)

  const location = useLocation()

  return token ? (
    props.children
  ) : (
    <Navigate to="/login" replace state={{ from: location.pathname }} />
  )
}
export default ProtectedRoute
