import PageFlipper from "@/Components/PageFlipper/PageFlipper"
import Footer from "@/Components/Footer/Footer"
import ChoreList from "@/Components/ChoreList/ChoreList"
import Navbar from "@/Components/Navbar/Navbar"

type Props = {}
const Home = (props: Props) => {
  return (
    <>
      <Navbar />
      <PageFlipper />
      <ChoreList />
      <Footer />
    </>
  )
}
export default Home
