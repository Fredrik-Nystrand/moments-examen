import { useState } from "react"
import { useLoginUserMutation } from "@/services/userService"
import { useAppDispatch } from "@/hooks/reduxHooks"
import { loginUser as loginUserAction } from "@/store/slices/usersSlice"
import { useNavigate } from "react-router-dom"

type Props = {}
const LoginForm = (props: Props) => {
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")
  const [loginUser, { isLoading }] = useLoginUserMutation()
  const dispatch = useAppDispatch()
  const navigate = useNavigate()

  const [loginError, setLoginError] = useState("")

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    let res
    try {
      res = await loginUser({ email, password }).unwrap()
    } catch (error: any) {
      console.log(error)
    }

    if (res) {
      dispatch(loginUserAction(res))
      navigate("/")
    }
  }

  return (
    <div className={`absolute left-0 right-0 top-1/2 -translate-y-1/2`}>
      <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-purple-dark">
            Logga in med ditt konto
          </h2>
          <p>{loginError}</p>
        </div>

        <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
          <form className="space-y-6" onSubmit={(e) => onSubmit(e)}>
            <div>
              <label
                htmlFor="email"
                className="text-gray-900 block text-sm font-medium leading-6"
              >
                Email address
              </label>
              <div className="mt-2">
                <input
                  id="email"
                  name="email"
                  type="email"
                  autoComplete="email"
                  required
                  onChange={(e) => setEmail(e.target.value)}
                  className="text-gray-900 placeholder:text-gray-400 focus:ring-indigo-600 block w-full rounded-md border-0 bg-grey-mid-1 py-1.5 shadow-sm ring-1 ring-inset ring-grey-mid-2 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <div className="flex items-center justify-between">
                <label
                  htmlFor="password"
                  className="text-gray-900 block text-sm font-medium leading-6"
                >
                  Lösenord
                </label>
              </div>
              <div className="mt-2">
                <input
                  id="password"
                  name="password"
                  type="password"
                  autoComplete="current-password"
                  onChange={(e) => setPassword(e.target.value)}
                  required
                  className="text-gray-900 placeholder:text-gray-400 focus:ring-indigo-600 block w-full rounded-md border-0 bg-grey-mid-1 py-1.5 shadow-sm ring-1 ring-inset ring-grey-mid-2 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <button
                type="submit"
                className="text-white focus-visible:outline-indigo-600 flex w-full justify-center rounded-md bg-gradient px-3 py-1.5 text-sm font-semibold leading-6 shadow-sm hover:bg-gradientRev focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2"
              >
                Logga In
              </button>
            </div>
          </form>

          <p className="text-gray-500 mt-10 text-center text-sm">
            Inte medlem än?{" "}
            <a
              href="#"
              className="text-indigo-600 hover:text-indigo-500 font-semibold leading-6"
            >
              Registrera dig här!
            </a>
          </p>
        </div>
      </div>
    </div>
  )
}
export default LoginForm
