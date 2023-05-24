import Root from "@/Routes/Root.tsx"
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom"
import { Provider } from "react-redux"
import store from "@/store/store.ts"
import Home from "@/views/Home/Home.tsx"
import Login from "@/views/Login/Login.tsx"
import ProtectedRoute from "./Routes/ProtectedRoute"

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<Root />}>
      <Route
        index
        element={
          <ProtectedRoute>
            <Home />
          </ProtectedRoute>
        }
      />
      <Route path="/login" element={<Login />} />
    </Route>
  )
)

function App() {
  return (
    <div className={`app bg-grey-dark`}>
      <Provider store={store}>
        <RouterProvider router={router} />
      </Provider>
    </div>
  )
}

export default App
