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
import Location from "../../models/location/Location";
import { setLocations, setSelectedLocation } from "../../store/locationSlice";

interface LocationDialogProps {
  open: boolean;
  onClose: () => void;
}

const LocationDialog = ({ open, onClose }: LocationDialogProps) => {
  const dispatch = useAppDispatch();
  const locations = useAppSelector((state) => state.locationReducer.locations);
  const selectedLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  useEffect(() => {
    const fetchLocations = async () => {
      const result = await axios.get<Location[]>("api/location");
      dispatch(setLocations(result.data));
      return result.data;
    };

    fetchLocations().then((fetched) => {
      dispatch(setSelectedLocation(fetched[0].id));
    });
  }, [dispatch]);

  const handleLocationChange = (event: SelectChangeEvent) => {
    dispatch(setSelectedLocation(event.target.value));
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Wybrana lokacja: </DialogTitle>
      <DialogContent>
        <FormControl sx={{ m: 1 }}>
          <InputLabel id="location-label">Lokacja</InputLabel>
          <Select
            labelId="location-label"
            sx={{ m: 1 }}
            value={selectedLocation?.id}
            label="Lokacja"
            onChange={handleLocationChange}
          >
            {locations.map((loc) => (
              <MenuItem value={loc.id}>{loc.name}</MenuItem>
            ))}
          </Select>
          <Button sx={{ m: 1 }} variant="contained">
            Zmień na wybraną
          </Button>
        </FormControl>
      </DialogContent>
    </Dialog>
  );
};

export default LocationDialog;
