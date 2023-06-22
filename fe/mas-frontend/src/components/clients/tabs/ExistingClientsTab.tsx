import { TextField } from "@mui/material";
import { useEffect, useState } from "react";
import IndividualClient from "../../../models/clients/IndividualClient";
import BusinessClient from "../../../models/clients/BusinessClient";
import Clients from "../Clients";
import axios from "axios";
import { useAppSelector } from "../../../hooks/redux";

const ExistingClientsTab = () => {
  const [individualClients, setIndividualClients] = useState<
    IndividualClient[]
  >([]);
  const [businessClients, setBusinessClients] = useState<BusinessClient[]>([]);
  const currentLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  useEffect(() => {
    const fetchIndividualClients = async () => {
      const result = await axios.get<IndividualClient[]>(
        `api/client/individual/${currentLocation?.id}`
      );
      setIndividualClients(result.data);
    };
    const fetchBusinessClients = async () => {
      const result = await axios.get<BusinessClient[]>(
        `api/client/business/${currentLocation?.id}`
      );
      setBusinessClients(result.data);
    };

    fetchIndividualClients();
    fetchBusinessClients();
  }, [currentLocation]);

  return (
    <>
      <TextField
        fullWidth
        id="client-search"
        variant="standard"
        label="Wyszukaj klienta"
      />
      <Clients individual={individualClients} business={businessClients} />
    </>
  );
};

export default ExistingClientsTab;
