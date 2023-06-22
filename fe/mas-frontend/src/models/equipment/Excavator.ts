import Location from "../location/Location";
import PieceOfEquipment from "./PieceOfEquipment";

interface Excavator extends PieceOfEquipment {
  armLength: number;
  isTracked: boolean;
  location: Location;
}

export default Excavator;
