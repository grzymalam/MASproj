import { DateRange } from "@mui/x-date-pickers-pro";
import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import BusinessClient from "../models/clients/BusinessClient";
import IndividualClient from "../models/clients/IndividualClient";

export interface ClientState {
  chosenClient: BusinessClient | IndividualClient | undefined;
}

const initialState: ClientState = {
  chosenClient: undefined,
};

export const clientSlice = createSlice({
  name: "clients",
  initialState,
  reducers: {
    setClient: (
      state,
      action: PayloadAction<BusinessClient | IndividualClient>
    ) => {
      return { chosenClient: action.payload };
    },
  },
});

export const { setClient } = clientSlice.actions;

export default clientSlice.reducer;
