import IndividualClient from "../../models/clients/IndividualClient";
import BusinessClient from "../../models/clients/BusinessClient";
import IndividualClientCard from "./cards/IndividualClientCard";
import BusinessClientCard from "./cards/BusinessClientCard";
import { Box } from "@mui/material";

interface ClientsProps {
  individual: IndividualClient[];
  business: BusinessClient[];
}

const Clients = ({ individual, business }: ClientsProps) => {
  const individualClientCards = individual.map((client) => (
    <IndividualClientCard client={client} key={client.id} />
  ));

  const businessClientCards = business.map((client) => (
    <BusinessClientCard client={client} key={client.id} />
  ));

  return (
    <Box sx={{ display: "flex", flexWrap: "wrap" }}>
      {individualClientCards}
      {businessClientCards}
    </Box>
  );
};

export default Clients;
