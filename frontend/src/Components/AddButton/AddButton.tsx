import { PlusSmallIcon } from "@heroicons/react/24/solid"

type Props = {}
const AddButton = (props: Props) => {
  return (
    <div className={`fixed bottom-4 rounded-full bg-gradient`}>
      <PlusSmallIcon className={`h-24 w-24`} />
    </div>
  )
}
export default AddButton
