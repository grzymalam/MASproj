import {
  Box,
  Button,
  Chip,
  Dialog,
  DialogContent,
  DialogTitle,
  FormControl,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { useEffect, useState } from "react";
import CancelIcon from "@mui/icons-material/Cancel";
import Accessory from "../../models/equipment/Accessory";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import axios from "axios";
import {
  setGlobalAccessories,
  setSelectedAccessories,
} from "../../store/accessoriesSlice";

interface AccessoriesDialogProps {
  open: boolean;
  onClose: () => void;
  callback: () => void;
  onAllSelected: () => void;
}

const AccessoriesDialog = ({
  open,
  onClose,
  callback,
  onAllSelected,
}: AccessoriesDialogProps) => {
  const selected = useAppSelector((state) => state.accessoriesReducer.selected);
  const accessories = useAppSelector(
    (state) => state.accessoriesReducer.accessories
  );

  const dispatch = useAppDispatch();

  const from = useAppSelector((state) => state.durationReducer.start);
  const to = useAppSelector((state) => state.durationReducer.end);
  const selectedPieceOfEquipment = useAppSelector(
    (state) => state.equipmentReducer.selected
  );
  const location = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  useEffect(() => {
    const fetch = async () => {
      const res = await axios.post<Accessory[]>("api/accessory", {
        from: new Date(from * 1000),
        to: new Date(to * 1000),
        pieceOfEquipmentId: selectedPieceOfEquipment?.id,
        location: location?.id,
      });
      if (res.data.length == 0) {
        callback();
        onClose();
      } else {
        dispatch(setGlobalAccessories(res.data));
      }
    };
    fetch();
  }, []);

  const changeHandler = (event: SelectChangeEvent<string[]>) => {
    const selectedIds = event.target.value;

    if (Array.isArray(selectedIds)) {
      dispatch(setSelectedAccessories(selectedIds));
    }
    if (typeof selectedIds === "string") {
      dispatch(setSelectedAccessories([selectedIds]));
    }
  };

  const closeHandler = () => {
    dispatch(setSelectedAccessories([]));
    onClose();
  };

  const confirmHandler = () => {
    closeHandler();
    onAllSelected();
  };

  return (
    <Dialog open={open} onClose={closeHandler}>
      <DialogTitle>Akcesoria</DialogTitle>
      <DialogContent>
        <FormControl>
          <Select
            sx={{ m: 1, width: 400 }}
            fullWidth
            multiple
            value={selected.map((acc) => acc.id)}
            onChange={changeHandler}
            renderValue={(s) => (
              <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                {s.map((value) => (
                  <Chip
                    key={value}
                    label={accessories.find((acc) => acc.id == value)?.name}
                    onDelete={() =>
                      dispatch(
                        setSelectedAccessories(
                          selected
                            .map((s) => s.id)
                            .filter((acc) => acc !== value)
                        )
                      )
                    }
                    deleteIcon={
                      <CancelIcon
                        onMouseDown={(event) => event.stopPropagation()}
                      />
                    }
                  />
                ))}
              </Box>
            )}
          >
            {accessories.map((acc) => (
              <MenuItem key={acc.id} value={acc.id}>
                {acc.name}
              </MenuItem>
            ))}
          </Select>
          <Button sx={{ m: 1 }} variant="contained" onClick={confirmHandler}>
            Potwierdź
          </Button>
        </FormControl>
      </DialogContent>
    </Dialog>
  );
};

export default AccessoriesDialog;
