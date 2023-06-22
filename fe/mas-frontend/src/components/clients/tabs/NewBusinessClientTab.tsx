import { Button, FormControl, TextField } from "@mui/material";
import axios from "axios";
import { FormEvent, useState } from "react";
import { useAppSelector } from "../../../hooks/redux";

interface NewBusinessClientProps {
  onClose: () => void;
}

const NewBusinessClientTab = ({ onClose }: NewBusinessClientProps) => {
  const [nip, setNip] = useState("");
  const [discount, setDiscount] = useState(0);
  const currentLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  const submitHandler = async (event: FormEvent) => {
    event.preventDefault();
    axios
      .post("api/client/business", {
        locationId: currentLocation?.id,
        nip,
        discount,
      })
      .then(() => onClose());
  };
  return (
    <>
      <form onSubmit={submitHandler}>
        <FormControl fullWidth>
          <TextField
            sx={{ m: 1 }}
            id="nip-field"
            variant="standard"
            label="NIP"
            value={nip}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setNip(event.target.value);
            }}
          />
          <TextField
            type="number"
            sx={{ m: 1 }}
            id="discount-field"
            variant="standard"
            label="Zniżka"
            value={discount}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setDiscount(+event.target.value);
            }}
          />
          <Button sx={{ m: 1 }} type="submit" variant="contained">
            Dodaj
          </Button>
        </FormControl>
      </form>
    </>
  );
};

export default NewBusinessClientTab;
