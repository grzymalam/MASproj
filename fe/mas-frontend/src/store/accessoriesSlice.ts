import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import dayjs from "dayjs";
import PieceOfEquipmentUnion from "../models/equipment/PieceOfEquipmentUnion";
import Accessory from "../models/equipment/Accessory";

export interface AccessoriesState {
  selected: Accessory[];
  accessories: Accessory[];
}

const initialState: AccessoriesState = {
  selected: [],
  accessories: [],
};

export const accessoriesSlice = createSlice({
  name: "accessories",
  initialState,
  reducers: {
    setSelectedAccessories: (state, action: PayloadAction<string[]>) => {
      console.log("setting: " + action.payload);
      const sel = state.accessories.filter((acc) =>
        action.payload.includes(acc.id)
      );
      return { ...state, selected: sel };
    },
    removeFromSelectedAccessories: (state, action: PayloadAction<string>) => {
      const newSelected = state.selected?.filter(
        (acc) => acc.id != action.payload
      );
      return { ...state, selected: newSelected };
    },
    setGlobalAccessories: (state, action: PayloadAction<Accessory[]>) => {
      return {
        ...state,
        accessories: action.payload,
      };
    },
  },
});

export const {
  setSelectedAccessories,
  removeFromSelectedAccessories,
  setGlobalAccessories,
} = accessoriesSlice.actions;

export default accessoriesSlice.reducer;
