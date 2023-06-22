import { CardHeader, Typography } from "@mui/material";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import IndividualClient from "../../../models/clients/IndividualClient";
import { useAppDispatch } from "../../../hooks/redux";
import { setClient } from "../../../store/clientSlice";

interface IndividualClientCardProps {
  client: IndividualClient;
}

const IndividualClientCard = ({ client }: IndividualClientCardProps) => {
  const dispatch = useAppDispatch();

  const clientSelectedHandler = () => {
    dispatch(setClient(client));
  };
  return (
    <Card sx={{ m: 1 / 2, p: 2, flexGrow: 1 }}>
      <Typography gutterBottom variant="h5" component="div">
        Indywidualny
      </Typography>
      <p>{client.name}</p>
      <p>{client.lastname}</p>
      <p>{client.pesel}</p>
      <Button variant="contained" onClick={clientSelectedHandler}>
        Wybierz
      </Button>
    </Card>
  );
};

export default IndividualClientCard;
