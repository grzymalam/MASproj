import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import Location from "../models/location/Location";
import Salesman from "../models/employee/Salesman";

export interface SalesmanState {
  selectedSalesman?: Salesman;
  salesmen: Salesman[];
}

const initialState: SalesmanState = {
  selectedSalesman: undefined,
  salesmen: [],
};

export const salesmenSlice = createSlice({
  name: "salesmen",
  initialState,
  reducers: {
    setSalesmen: (state, action: PayloadAction<Salesman[]>) => {
      return { ...state, salesmen: action.payload };
    },
    setSelectedSalesman: (state, action: PayloadAction<string>) => {
      const location = state.salesmen.find(
        (salesman) => salesman.salesmanId === action.payload
      );
      return { ...state, selectedSalesman: location };
    },
  },
});

export const { setSalesmen, setSelectedSalesman } = salesmenSlice.actions;

export default salesmenSlice.reducer;
