import { Box } from "@mui/material";
import { useEffect, useState } from "react";
import EquipmentCard from "./EquipmentCard";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import axios from "axios";
import FetchedPiecesOfEquipment from "../../models/equipment/FetchedPiecesOfEquipment";
import { setPiecesOfEquipment } from "../../store/equipmentSlice";

interface EquipmentProps {
  onSelected: (isTransportRequired: boolean) => void;
}

const Equipment = ({ onSelected }: EquipmentProps) => {
  const start = useAppSelector((state) => state.durationReducer.start);
  const end = useAppSelector((state) => state.durationReducer.end);
  const client = useAppSelector((state) => state.clientReducer.chosenClient);
  const pieces = useAppSelector(
    (state) => state.equipmentReducer.piecesOfEquipment
  );
  const dispatch = useAppDispatch();
  useEffect(() => {
    const fetchEquipment = async () => {
      const result = await axios.post<FetchedPiecesOfEquipment>(
        "api/equipment",
        {
          from: new Date(start),
          to: new Date(end),
        }
      );

      dispatch(
        setPiecesOfEquipment([
          ...result.data.excavators,
          ...result.data.loaders,
          ...result.data.dumpTrucks,
        ])
      );
    };

    fetchEquipment();
  }, [start, end, client]);

  if (client === undefined) return <p>Wybierz klienta...</p>;
  const cards = pieces.map((piece) => (
    <EquipmentCard
      key={piece.id}
      onSelected={onSelected}
      pieceOfEquipment={piece}
    />
  ));
  if (pieces.length === 0) return <p>Wybierz klienta i datę...</p>;
  return <Box sx={{ display: "flex", flexWrap: "wrap" }}>{cards}</Box>;
};

export default Equipment;
