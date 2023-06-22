import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import dayjs from "dayjs";

export interface DurationState {
  start: number;
  end: number;
}

const initialState: DurationState = {
  start: dayjs().unix(),
  end: dayjs().unix(),
};

export const durationState = createSlice({
  name: "duration",
  initialState,
  reducers: {
    setStart: (state, action: PayloadAction<number>) => {
      return { ...state, start: action.payload };
    },
    setEnd: (state, action: PayloadAction<number>) => {
      return { ...state, end: action.payload };
    },
  },
});

export const { setStart, setEnd } = durationState.actions;

export default durationState.reducer;
