import { Button, Dialog, DialogContent, DialogTitle } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import dayjs from "dayjs";
import axios from "axios";
import { removePieceOfEquipment } from "../../store/equipmentSlice";

interface ConfirmationDialogProps {
  open: boolean;
  onClose: () => void;
}

const ConfirmationDialog = ({ open, onClose }: ConfirmationDialogProps) => {
  const start = useAppSelector((state) => state.durationReducer.start);
  const end = useAppSelector((state) => state.durationReducer.end);
  const pieceOfEquipment = useAppSelector(
    (state) => state.equipmentReducer.selected
  );
  const accessories = useAppSelector(
    (state) => state.accessoriesReducer.selected
  );
  const client = useAppSelector((state) => state.clientReducer.chosenClient);
  const salesman = useAppSelector(
    (state) => state.salesmenReducer.selectedSalesman
  );

  const dispatch = useAppDispatch();

  const rentHandler = () => {
    axios
      .post("api/rental", {
        salesmanId: salesman?.salesmanId,
        clientId: client?.id,
        pieceOfEquipmentId: pieceOfEquipment?.id,
        accessoryIds: accessories.map((acc) => acc.id),
        from: dayjs.unix(start),
        to: dayjs.unix(end),
      })
      .then(() => {
        dispatch(removePieceOfEquipment(pieceOfEquipment!.id));
        onClose();
      });
  };
  const accessoriesAsElements =
    accessories.length === 0
      ? "brak"
      : accessories.map((acc) => <p>{acc.name}</p>);
  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Dane wypożyczenia</DialogTitle>
      <DialogContent>
        <p>Start: {dayjs.unix(start).toISOString()}</p>
        <p>Koniec: {dayjs.unix(end).toISOString()}</p>
        <p>Nazwa sprzętu: {pieceOfEquipment?.name}</p>
        <p>Akcesoria: {accessoriesAsElements}</p>
        <Button variant="contained" onClick={rentHandler}>
          Potwierdź
        </Button>
      </DialogContent>
    </Dialog>
  );
};

export default ConfirmationDialog;
