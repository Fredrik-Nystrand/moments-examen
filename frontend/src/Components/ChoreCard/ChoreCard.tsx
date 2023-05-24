import { ReactComponent as GymIcon } from "@/assets/svg/dumbell.svg"
import { ClockIcon } from "@heroicons/react/24/solid"

type Props = {
  name: string
  xp: number
  hours: string
  isTimebased: boolean
}
const ChoreCard = ({ name, xp, hours, isTimebased }: Props) => {
  return (
    <div className={`my-4 flex items-center rounded-xl bg-gradient`}>
      <div
        className={`flex h-full w-24 items-center justify-center rounded-l-xl bg-black-half px-6 text-grey-light`}
      >
        <GymIcon className={`icon-white h-10 w-10 -rotate-45`} />
      </div>
      <div
        className={`divider-h flex h-16 flex-1 flex-col items-center justify-center`}
      >
        <div>
          <p>{name}</p>
        </div>
        {isTimebased && (
          <div className={`flex gap-1`}>
            <div className={`flex -translate-y-px items-center justify-center`}>
              <ClockIcon className={`h-4 w-4`} />
            </div>
            <div className={`flex translate-y-px items-center justify-center`}>
              <p>{hours}H</p>
            </div>
          </div>
        )}
      </div>
      <div
        className={`text-gray-light flex h-full w-24 flex-col items-center justify-center px-6 font-bold`}
      >
        <div>
          <p>{xp}</p>
        </div>
        <div>
          <p>XP</p>
        </div>
      </div>
    </div>
  )
}
export default ChoreCard
