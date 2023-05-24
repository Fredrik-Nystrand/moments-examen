import ChoreCard from "../ChoreCard/ChoreCard"
import styles from "./ChoreList.module.css"
import { useGetCompletedChoresQuery } from "@/services/choresServices"
import { useAppSelector } from "@/hooks/reduxHooks"

type Props = {}
const ChoreList = (props: Props) => {
  const id = useAppSelector((state) => state.user.user?.id)

  const { data } = useGetCompletedChoresQuery(id)
  return (
    <div
      className={`${styles.wrapper} scrollbar container flex flex-col px-5 pb-12`}
    >
      {data?.map(({ id, choreName, amount, completedXp, isTimebased }) => (
        <ChoreCard
          key={id}
          name={choreName}
          hours={amount}
          xp={completedXp}
          isTimebased={isTimebased}
        />
      ))}
    </div>
  )
}
export default ChoreList
