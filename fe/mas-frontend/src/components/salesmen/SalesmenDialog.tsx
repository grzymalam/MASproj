import {
  Button,
  Dialog,
  DialogContent,
  DialogTitle,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import { useEffect } from "react";
import axios from "axios";
import Salesman from "../../models/employee/Salesman";
import { setSalesmen, setSelectedSalesman } from "../../store/salesmenSlice";

interface SalesmenDialogProps {
  open: boolean;
  onClose: () => void;
}

const SalesmenDialog = ({ open, onClose }: SalesmenDialogProps) => {
  const dispatch = useAppDispatch();
  const salesmen = useAppSelector((state) => state.salesmenReducer.salesmen);
  const selectedSalesman = useAppSelector(
    (state) => state.salesmenReducer.selectedSalesman
  );
  const selectedLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  useEffect(() => {
    const fetchSalesmen = async () => {
      const result = await axios.get<Salesman[]>(
        `api/salesmen/${selectedLocation?.id}`
      );
      dispatch(setSalesmen(result.data));
      return result.data;
    };

    fetchSalesmen().then((fetched) => {
      dispatch(setSelectedSalesman(fetched[0].salesmanId));
    });
  }, [selectedLocation]);

  const handleLocationChange = (event: SelectChangeEvent) => {
    dispatch(setSelectedSalesman(event.target.value));
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Wybrany sprzedawca: </DialogTitle>
      <DialogContent>
        <FormControl sx={{ m: 1 }}>
          <InputLabel id="salesmen-label">Sprzedawcy</InputLabel>
          <Select
            labelId="salesmen-label"
            sx={{ m: 1 }}
            value={selectedSalesman?.salesmanId}
            label="Lokacja"
            onChange={handleLocationChange}
          >
            {salesmen.map((salesman) => (
              <MenuItem
                value={salesman.salesmanId}
              >{`${salesman.name} ${salesman.lastname}`}</MenuItem>
            ))}
          </Select>
          <Button sx={{ m: 1 }} variant="contained">
            Zmień na wybranego
          </Button>
        </FormControl>
      </DialogContent>
    </Dialog>
  );
};

export default SalesmenDialog;
