import { Button, FormControl, TextField } from "@mui/material";
import axios from "axios";
import { FormEvent, useState } from "react";
import { useAppSelector } from "../../../hooks/redux";

interface NewIndividualClientProps {
  onClose: () => void;
}

const NewIndividualClientTab = ({ onClose }: NewIndividualClientProps) => {
  const [pesel, setPesel] = useState("");
  const [name, setName] = useState("");
  const [lastname, setLastname] = useState("");
  const currentLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  const submitHandler = async (event: FormEvent) => {
    event.preventDefault();
    axios
      .post("api/client/individual", {
        locationId: currentLocation?.id,
        pesel,
        name,
        lastname,
      })
      .then(() => onClose());
  };
  return (
    <>
      <form onSubmit={submitHandler}>
        <FormControl fullWidth>
          <TextField
            sx={{ m: 1 }}
            id="pesel-field"
            variant="standard"
            label="PESEL"
            value={pesel}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setPesel(event.target.value);
            }}
          />
          <TextField
            sx={{ m: 1 }}
            id="name-field"
            variant="standard"
            label="Imię"
            value={name}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setName(event.target.value);
            }}
          />
          <TextField
            sx={{ m: 1 }}
            id="lastname-field"
            variant="standard"
            label="Nazwisko"
            value={lastname}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setLastname(event.target.value);
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

export default NewIndividualClientTab;
