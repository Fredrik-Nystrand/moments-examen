import { CurrencyDollarIcon, StarIcon } from "@heroicons/react/24/solid"
import AddButton from "../AddButton/AddButton"
import styles from "./Footer.module.css"
import Modal from "../Modal/Modal"
import { useState } from "react"
import CompleteChoreForm from "../CompleteChoreForm/CompleteChoreForm"

type Props = {}
const Footer = (props: Props) => {
  const [open, setOpen] = useState(false)
  return (
    <>
      <Modal
        modalOpen={open}
        setModalOpen={setOpen}
        element={<CompleteChoreForm modalOpen={setOpen} />}
      />
      <div className={`fixed bottom-0 left-0 right-0 ${styles.shadow}`}>
        <div className={`mt-12 bg-grey-mid-1 px-5 py-2 md:px-0 `}>
          <div className={`container flex`}>
            <div className={`flex flex-1 flex-col items-center justify-center`}>
              <CurrencyDollarIcon className={`h-8 w-8`} />
              <p>Shop</p>
            </div>
            <button
              onClick={() => setOpen((state) => !state)}
              className={`flex flex-1 items-center justify-center`}
            >
              <AddButton />
            </button>
            <button
              className={`flex flex-1 flex-col items-center justify-center`}
            >
              <StarIcon className={`h-8 w-8`} />
              <p>Bonus</p>
            </button>
          </div>
        </div>
      </div>
    </>
  )
}
export default Footer
