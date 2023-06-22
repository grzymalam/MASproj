import { configureStore } from "@reduxjs/toolkit";
import locationReducer from "./locationSlice";
import durationReducer from "./durationSlice";
import clientReducer from "./clientSlice";
import equipmentReducer from "./equipmentSlice";
import salesmenReducer from "./salesmenSlice";
import accessoriesReducer from "./accessoriesSlice";

export const store = configureStore({
  reducer: {
    locationReducer,
    durationReducer,
    clientReducer,
    equipmentReducer,
    salesmenReducer,
    accessoriesReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;
