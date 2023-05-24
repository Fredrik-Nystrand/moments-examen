import { useCallback } from "react"
import styles from "./ProgressBar.module.css"

type Props = {
  currentLevel: number
  currentXp: number
}

const ProgressBar = ({ currentXp, currentLevel }: Props) => {
  const maximumXp: number = 5000

  const calcPercentage = useCallback((): number => {
    return (currentXp / maximumXp) * 100
  }, [currentXp])

  return (
    <div className={`flex items-center gap-2`}>
      <p className={`w-9 text-center `}>{currentLevel}</p>
      <div className={`${styles.outer}`}>
        <div
          className={`${styles.inner}`}
          style={{ width: calcPercentage() + "%" }}
        ></div>
      </div>
      <p className={`w-9 text-center `}>{currentLevel + 1}</p>
    </div>
  )
}
export default ProgressBar
