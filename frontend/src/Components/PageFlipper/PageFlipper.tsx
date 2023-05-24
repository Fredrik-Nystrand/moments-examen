import { ChevronLeftIcon, ChevronRightIcon } from "@heroicons/react/20/solid"

type Props = {}
const PageFlipper = (props: Props) => {
  return (
    <div
      className={`container fixed left-0 right-0 top-64 z-10 my-5 flex h-16 items-center px-5 md:px-0`}
    >
      <button className="text-gray-light h-full rounded-l-xl bg-grey-mid-1 px-6 focus:z-20 focus:outline-offset-0">
        <span className="sr-only">Previous</span>
        <ChevronLeftIcon className="h-8 w-8" aria-hidden="true" />
      </button>
      <div
        className={`flex h-full flex-1 items-center justify-center bg-grey-mid-1 px-2`}
      >
        <p>Dagens Moment</p>
      </div>
      <button className="h-full rounded-r-xl bg-grey-mid-1 px-6 text-grey-light focus:z-20 focus:outline-offset-0">
        <span className="sr-only">Next</span>
        <ChevronRightIcon className=" h-8 w-8" aria-hidden="true" />
      </button>
    </div>
  )
}
export default PageFlipper
