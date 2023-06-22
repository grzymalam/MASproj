import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import Location from "../models/location/Location";

export interface LocationState {
  selectedLocation?: Location;
  locations: Location[];
}

const initialState: LocationState = {
  selectedLocation: undefined,
  locations: [],
};

export const locationSlice = createSlice({
  name: "locations",
  initialState,
  reducers: {
    setLocations: (state, action: PayloadAction<Location[]>) => {
      return { ...state, locations: action.payload };
    },
    setSelectedLocation: (state, action: PayloadAction<string>) => {
      const location = state.locations.find((loc) => loc.id === action.payload);
      return { ...state, selectedLocation: location };
    },
  },
});

export const { setLocations, setSelectedLocation } = locationSlice.actions;

export default locationSlice.reducer;
