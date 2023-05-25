import { UserIcon, Cog8ToothIcon } from "@heroicons/react/24/solid"
import { ReactComponent as CoinsIcon } from "@/assets/svg/coins.svg"
import ProgressBar from "../ProgressBar/ProgressBar"
import { useAppSelector } from "@/hooks/reduxHooks"
import type { User } from "@/assets/types"

type Props = {}

const Navbar = (props: Props) => {
  const userData: User = useAppSelector((state) => state.user.user)
  return (
    <div className={`fixed left-0 right-0 top-0 z-10 bg-grey-dark`}>
      <div className={`mb-14 rounded-b-3xl bg-gradient py-4`}>
        <div className={`container flex flex-col gap-6`}>
          <div className={`flex items-center justify-between px-5 md:px-0`}>
            <button>
              <Cog8ToothIcon className={`h-8 w-8`} />
            </button>
            <div className={`flex items-center gap-2`}>
              <CoinsIcon className={`h-8 w-8`} />
              <p>{userData.currency}</p>
            </div>
            <button>
              <UserIcon className={`h-8 w-8`} />
            </button>
          </div>

          <div className={`flex flex-col`}>
            <div className={`pb-4`}>
              <h3 className={`text-center`}>Framgångspoäng</h3>
            </div>
            <div className={`flex px-5 md:px-0`}>
              <div className={`divider-h flex flex-1 justify-start`}>
                <div className={`flex flex-col items-center`}>
                  <p>{userData.xpLastMonth} XP</p>
                  <p className={`text-xs`}>Förra månaden</p>
                </div>
              </div>
              <div className={`divider-h flex flex-1 justify-center`}>
                <div className={`flex flex-col items-center`}>
                  <p>{userData.xpToday ? userData.xpToday : 0} XP</p>
                  <p className={`text-xs`}>Idag</p>
                </div>
              </div>
              <div className={`flex flex-1 justify-end`}>
                <div className={`flex flex-col items-center`}>
                  <p>{userData.xpThisMonth} XP</p>
                  <p className={`text-xs`}>Denna månaden</p>
                </div>
              </div>
            </div>
          </div>

          <div className={`px-5 md:px-0`}>
            <h3 className={`pb-2 text-center`}>Nivå</h3>
            <ProgressBar
              currentLevel={userData.level ? userData.level : 0}
              currentXp={userData.xp ? userData.xp : 0}
            />
          </div>
        </div>
      </div>
    </div>
  )
}

export default Navbar
