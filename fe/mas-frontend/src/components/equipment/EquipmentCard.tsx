import { Button, Card } from "@mui/material";
import PieceOfEquipmentUnion from "../../models/equipment/PieceOfEquipmentUnion";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import { setSelectedEquipmentPiece } from "../../store/equipmentSlice";

interface EquipmentCardProps {
  pieceOfEquipment: PieceOfEquipmentUnion;
  onSelected: (isTransportRequired: boolean) => void;
}

const EquipmentCard = ({
  pieceOfEquipment,
  onSelected,
}: EquipmentCardProps) => {
  const dispatch = useAppDispatch();
  const selected = useAppSelector((state) => state.equipmentReducer.selected);
  const selectedLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  const data = Object.keys(pieceOfEquipment).map((key, index) => {
    return (
      <p key={key}>{`${key}: ${Object.values(pieceOfEquipment)[index]}`}</p>
    );
  });

  const equipmentChosenHandler = () => {
    dispatch(setSelectedEquipmentPiece(pieceOfEquipment.id));
    onSelected(!(selectedLocation?.id === pieceOfEquipment.location.id));
  };
  const isThisPieceOfEquipmentSelected = selected?.id === pieceOfEquipment.id;
  return (
    <Card sx={{ m: 1, flexGrow: 1, p: 1, flexBasis: 200 }}>
      {isThisPieceOfEquipmentSelected && <CheckCircleIcon />}
      {data}
      <Button variant="contained" onClick={equipmentChosenHandler}>
        Wybierz
      </Button>
    </Card>
  );
};

export default EquipmentCard;
