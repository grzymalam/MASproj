import { CardHeader, Typography } from "@mui/material";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import BusinessClient from "../../../models/clients/BusinessClient";
import { useAppDispatch } from "../../../hooks/redux";
import { setClient } from "../../../store/clientSlice";

interface BusinessClientCardProps {
  client: BusinessClient;
}

const BusinessClientCard = ({ client }: BusinessClientCardProps) => {
  const dispatch = useAppDispatch();

  const clientSelectedHandler = () => {
    dispatch(setClient(client));
  };

  return (
    <Card sx={{ m: 1 / 2, p: 2, flexGrow: 1 }}>
      <Typography gutterBottom variant="h5" component="div">
        Biznesowy
      </Typography>
      <p>{client.name}</p>
      <p>{client.nip}</p>
      <Button variant="contained" onClick={clientSelectedHandler}>
        Wybierz
      </Button>
    </Card>
  );
};

export default BusinessClientCard;
