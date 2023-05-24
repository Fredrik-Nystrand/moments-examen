import { useRouteError } from "react-router-dom"

export default function ErrorPage() {
  const error = useRouteError()
  console.error(error)

  return (
    <div id="error-page" className={`app bg-grey-dark`}>
      <h1>Oops!</h1>
      <p>Sorry, an unexpected error has occurred.</p>
      <p></p>
    </div>
  )
}
