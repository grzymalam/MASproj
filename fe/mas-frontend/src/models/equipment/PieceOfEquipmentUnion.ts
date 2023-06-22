import DumpTruck from "./DumpTruck";
import Excavator from "./Excavator";
import Loader from "./Loader";

type PieceOfEquipmentUnion = Excavator | Loader | DumpTruck;

export default PieceOfEquipmentUnion;
