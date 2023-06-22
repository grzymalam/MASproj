import DumpTruck from "./DumpTruck";
import Excavator from "./Excavator";
import Loader from "./Loader";

interface FetchedPiecesOfEquipment {
  excavators: Excavator[];
  loaders: Loader[];
  dumpTrucks: DumpTruck[];
}

export default FetchedPiecesOfEquipment;
