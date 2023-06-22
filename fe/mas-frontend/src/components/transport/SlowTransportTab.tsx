import { Box, Button } from "@mui/material";
import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import AutorenewIcon from "@mui/icons-material/Autorenew";
import axios from "axios";
import { removePieceOfEquipment } from "../../store/equipmentSlice";

interface SlowTransportTabProps {
  onClose: () => void;
}

const SlowTransportTab = ({ onClose }: SlowTransportTabProps) => {
  const [estimatedPrice, setEstimatedPrice] = useState(0);
  const [isLoading, setIsLoading] = useState(false);

  const dispatch = useAppDispatch();
  const client = useAppSelector((state) => state.clientReducer.chosenClient);
  const selectedPiece = useAppSelector(
    (state) => state.equipmentReducer.selected
  );
  const currentLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  useEffect(() => {
    const fetchEstimate = async () => {
      const result = await axios.post<TransportDto>(
        "api/transport/estimate/slow",
        {
          pieceOfEquipmentId: selectedPiece?.id,
          clientId: client?.id,
          fromLocationId: selectedPiece?.location.id,
          toLocationId: currentLocation?.id,
        }
      );
      return result.data;
    };

    fetchEstimate().then((fetched) => {
      setEstimatedPrice(fetched.cost);
    });
  }, []);

  const orderHandler = async () => {
    setIsLoading(true);
    axios
      .post<TransportDto>("api/transport/slow", {
        pieceOfEquipmentId: selectedPiece?.id,
        clientId: client?.id,
        fromLocationId: selectedPiece?.location.id,
        toLocationId: currentLocation?.id,
      })
      .then(() => {
        dispatch(removePieceOfEquipment(selectedPiece!.id));
        onClose();
      })
      .finally(() => setIsLoading(false));
  };

  return (
    <Box>
      <p>Z: {selectedPiece?.location.name}</p>
      <p>Do: {currentLocation?.name}</p>
      <p>Cena: {estimatedPrice}</p>
      <Button onClick={orderHandler} variant="contained" disabled={isLoading}>
        {isLoading ? (
          <AutorenewIcon
            sx={{
              animation: "spin 1s linear infinite",
              "@keyframes spin": {
                "0%": {
                  transform: "rotate(0deg)",
                },
                "100%": {
                  transform: "rotate(360deg)",
                },
              },
            }}
          />
        ) : (
          "Zamów"
        )}
      </Button>
    </Box>
  );
};

export default SlowTransportTab;
