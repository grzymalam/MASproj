import Location from "../location/Location";
import LoaderType from "./LoaderType";
import PieceOfEquipment from "./PieceOfEquipment";

interface Loader extends PieceOfEquipment {
  loaderType: LoaderType;
  width: number;
  location: Location;
}

export default Loader;
