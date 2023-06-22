import Location from "../location/Location";
import PieceOfEquipment from "./PieceOfEquipment";

interface DumpTruck extends PieceOfEquipment {
  loadCapacity: number;
  maxSpeed: number;
  location: Location;
}

export default DumpTruck;
