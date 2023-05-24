import {
  useGetChoresQuery,
  useCompleteChoreMutation,
} from "@/services/choresServices"
import type {
  ComlpetedChoreRequest,
  User,
  Chore,
  MathOperators,
} from "@/assets/types"
import { useAppDispatch, useAppSelector } from "@/hooks/reduxHooks"
import { updateInfo } from "@/store/slices/completeChoreSlice"
import { useEffect, useState } from "react"
import { PlusSmallIcon, MinusSmallIcon } from "@heroicons/react/24/solid"
import { updateUser } from "@/store/slices/usersSlice"
import { useUpdateUserMutation } from "@/services/userService"

type Props = {
  modalOpen: React.Dispatch<React.SetStateAction<boolean>>
}
const CompleteChoreForm = ({ modalOpen }: Props) => {
  const { data: choreData } = useGetChoresQuery(undefined)
  const dispatch = useAppDispatch()
  const [submitCompletedChore] = useCompleteChoreMutation()
  const [getUserData] = useUpdateUserMutation()

  const completeChoreInfo: ComlpetedChoreRequest = useAppSelector(
    (state) => state.completeChore.completeChore
  )
  const user: User = useAppSelector((state) => state.user.user)
  const date = new Date(Date.now())

  const [isTimebased, setIsTimebased] = useState(false)
  const [hours, setHours] = useState(1)

  const addZero = (input: number): string => {
    return input + 1 > 9 ? `${input + 1}` : `0${input + 1}`
  }

  const today = `${date.getFullYear()}-${addZero(
    date.getMonth()
  )}-${date.getDate()}`

  useEffect(() => {
    dispatch(
      updateInfo({
        ...completeChoreInfo,
        dateCompleted: today,
        userId: user.id,
        amount: isTimebased ? hours.toString() : null,
      })
    )
  }, [isTimebased])

  useEffect(() => {
    return () => {
      dispatch(
        updateInfo({
          choreId: null,
          dateCompleted: null,
          userId: null,
          amount: null,
        })
      )
    }
  }, [])

  useEffect(() => {
    const selectedChore: Chore | undefined = choreData?.find(
      (chore) => chore.id === completeChoreInfo.choreId
    )

    setIsTimebased(selectedChore?.isTimebased ? true : false)
  }, [completeChoreInfo.choreId])

  const updateHours = (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    addOrSub: MathOperators
  ) => {
    e.preventDefault()

    const currentHours = hours
    let newHours

    if (addOrSub.operation === "Add") {
      newHours = currentHours + 0.5
    } else {
      newHours = currentHours - 0.5 < 0.5 ? 0.5 : currentHours - 0.5
    }

    if (currentHours !== newHours) {
      setHours(newHours)
      dispatch(
        updateInfo({
          ...completeChoreInfo,
          amount: newHours.toString(),
        })
      )
    }
  }

  const onSubmit = async () => {
    if (!completeChoreInfo.choreId) return
    if (!completeChoreInfo.userId) return
    if (!completeChoreInfo.dateCompleted) return
    if (isTimebased) {
      if (!completeChoreInfo.amount) return
    }

    const res = await submitCompletedChore(completeChoreInfo).unwrap()

    if (res) {
      modalOpen(false)
      const userData = await getUserData(res.userId).unwrap()

      if (userData) dispatch(updateUser(userData))
    }
  }

  return (
    <>
      <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-purple-light">
        Registrera ny aktivitet
      </h2>
      <form className="m-8 space-y-6" onSubmit={(e) => ""}>
        <div>
          <label
            htmlFor="chore"
            className="text-gray-900 block text-sm font-medium leading-6"
          >
            Vad har du gjort?
          </label>
          <div className="mt-2">
            <select
              id="chore"
              name="chore"
              onChange={(e) =>
                dispatch(
                  updateInfo({
                    ...completeChoreInfo,
                    choreId: parseInt(e.target.value),
                  })
                )
              }
              className="text-gray-900 placeholder:text-gray-400 focus:ring-indigo-600 block w-full rounded-md border-0 bg-grey-mid-2 py-1.5 shadow-sm ring-1 ring-inset ring-grey-mid-1 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
            >
              <option>Välj</option>
              {choreData?.map((chore) => (
                <option key={chore.id} value={chore.id}>
                  {chore.name}
                </option>
              ))}
            </select>
          </div>
        </div>
        <div>
          <label
            htmlFor="date"
            className="text-gray-900 block text-sm font-medium leading-6"
          >
            Datum
          </label>
          <div className="mt-2">
            <input
              id="date"
              name="date"
              type="date"
              value={
                completeChoreInfo.dateCompleted
                  ? completeChoreInfo.dateCompleted
                  : ""
              }
              onChange={(e) =>
                dispatch(
                  updateInfo({
                    ...completeChoreInfo,
                    dateCompleted: e.target.value,
                  })
                )
              }
              className="text-gray-900 placeholder:text-gray-400 focus:ring-indigo-600 block w-full rounded-md border-0 bg-grey-mid-2 py-1.5 shadow-sm ring-1 ring-inset ring-grey-mid-1 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6"
            />
          </div>
        </div>
        {isTimebased && (
          <div className={`flex flex-col justify-center`}>
            <label
              htmlFor="amount"
              className="text-gray-900 block text-center text-sm font-medium leading-6"
            >
              Hur länge?
            </label>
            <div className="mt-2 flex justify-center">
              <button
                className={`rounded-l-md bg-grey-mid-2 p-4 shadow-sm ring-1 ring-inset ring-grey-mid-1 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6`}
                onClick={(e) => updateHours(e, { operation: "Sub" })}
              >
                <MinusSmallIcon className={`h-5 w-5`} />
              </button>
              <div
                className={` flex w-20 justify-center bg-grey-mid-2 p-4 shadow-sm ring-1 ring-inset ring-grey-mid-1 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6`}
              >
                {hours}
              </div>
              <button
                className={`rounded-r-md bg-grey-mid-2 p-4 shadow-sm ring-1 ring-inset ring-grey-mid-1 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6`}
                onClick={(e) => updateHours(e, { operation: "Add" })}
              >
                <PlusSmallIcon className={`h-5 w-5`} />
              </button>
            </div>
          </div>
        )}
      </form>
      <div className="bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6">
        <button
          type="button"
          className="text-white inline-flex w-full justify-center rounded-md bg-gradient px-3 py-2 text-sm font-semibold shadow-sm hover:bg-gradientRev sm:ml-3 sm:w-auto"
          onClick={onSubmit}
        >
          Spara
        </button>
        <button
          type="button"
          className="text-gray-900 mt-3 inline-flex w-full justify-center rounded-md bg-grey-dark px-3 py-2 text-sm font-semibold hover:bg-grey-mid-2 sm:mt-0 sm:w-auto"
          onClick={() => modalOpen(false)}
        >
          Avbryt
        </button>
      </div>
    </>
  )
}
export default CompleteChoreForm
