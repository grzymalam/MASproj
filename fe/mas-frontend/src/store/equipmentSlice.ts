import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import dayjs from "dayjs";
import PieceOfEquipmentUnion from "../models/equipment/PieceOfEquipmentUnion";

export interface EquipmentState {
  selected?: PieceOfEquipmentUnion;
  piecesOfEquipment: PieceOfEquipmentUnion[];
}

const initialState: EquipmentState = {
  selected: undefined,
  piecesOfEquipment: [],
};

export const equipmentState = createSlice({
  name: "equipment",
  initialState,
  reducers: {
    setSelectedEquipmentPiece: (state, action: PayloadAction<string>) => {
      const piece = state.piecesOfEquipment.find(
        (p) => p.id === action.payload
      );
      return { ...state, selected: piece };
    },
    setPiecesOfEquipment: (
      state,
      action: PayloadAction<PieceOfEquipmentUnion[]>
    ) => {
      return { ...state, piecesOfEquipment: action.payload };
    },
    addPiecesOfEquipment: (
      state,
      action: PayloadAction<PieceOfEquipmentUnion[]>
    ) => {
      return {
        ...state,
        piecesOfEquipment: [...state.piecesOfEquipment, ...action.payload],
      };
    },
    removePieceOfEquipment: (state, action: PayloadAction<string>) => {
      const newPieces = state.piecesOfEquipment.filter(
        (p) => p.id !== action.payload
      );
      return { ...state, piecesOfEquipment: newPieces };
    },
  },
});

export const {
  setSelectedEquipmentPiece,
  setPiecesOfEquipment,
  addPiecesOfEquipment,
  removePieceOfEquipment,
} = equipmentState.actions;

export default equipmentState.reducer;
