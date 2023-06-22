import { useState } from "react";
import "./App.css";
import ClientsDialog from "./components/clients/ClientsDialog";
import IconButton from "@mui/material/IconButton";
import RecentActorsIcon from "@mui/icons-material/RecentActors";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import ConstructionIcon from "@mui/icons-material/Construction";
import DateRangeDialog from "./components/date_range/DateRangeDialog";
import Equipment from "./components/equipment/Equipment";
import TransportDialog from "./components/transport/TransportDialog";
import AccessoriesDialog from "./components/accessories/AccessoriesDialog";
import LocationOnIcon from "@mui/icons-material/LocationOn";
import PersonIcon from "@mui/icons-material/Person";
import ConfirmationDialog from "./components/confirmation/ConfirmationDialog";
import LocationDialog from "./components/location/LocationDialog";
import { useAppSelector } from "./hooks/redux";
import SalesmenDialog from "./components/salesmen/SalesmenDialog";

function App() {
  const [open, setOpen] = useState(false);
  const [dateRangeOpen, setDateRangeOpen] = useState(false);
  const [transportOpen, setTransportOpen] = useState(false);
  const [accessoriesOpen, setAccessoriesOpen] = useState(false);
  const [confirmationOpen, setConfirmationOpen] = useState(false);
  const [locationOpen, setLocationOpen] = useState(false);
  const [salesmenOpen, setSalesmenOpen] = useState(false);

  const selectedLocation = useAppSelector(
    (state) => state.locationReducer.selectedLocation
  );

  const equipmentSelectedHandler = (isTransportRequired: boolean) => {
    if (isTransportRequired) setTransportOpen(true);
    if (!isTransportRequired) {
      setAccessoriesOpen(true);
    }
  };

  const openRentalConirmationHandler = () => {
    setConfirmationOpen(true);
  };

  const clientsDialogCloseHandler = () => {
    setOpen(false);
  };

  const clientsDialogOpenHandler = () => {
    setOpen(true);
  };

  const dateRangeDialogCloseHandler = () => {
    setDateRangeOpen(false);
  };

  const dateRangeDialogOpenHandler = () => {
    setDateRangeOpen(true);
  };

  const transportCloseHandler = () => {
    setTransportOpen(false);
  };

  const accessoriesCloseHandler = () => {
    setAccessoriesOpen(false);
  };

  const accessoriesOpenHandler = () => {
    setAccessoriesOpen(true);
  };

  const confirmationCloseHandler = () => {
    setConfirmationOpen(false);
  };

  const confirmationOpenHandler = () => {
    setConfirmationOpen(true);
  };

  const locationCloseHandler = () => {
    setLocationOpen(false);
  };

  const locationOpenHandler = () => {
    setLocationOpen(true);
  };

  const salesmenOpenHandler = () => {
    setSalesmenOpen(true);
  };

  const salesmenCloseHandler = () => {
    setSalesmenOpen(false);
  };

  return (
    <div>
      <IconButton onClick={clientsDialogOpenHandler}>
        <RecentActorsIcon />
      </IconButton>
      <IconButton onClick={dateRangeDialogOpenHandler}>
        <CalendarMonthIcon />
      </IconButton>
      <IconButton
        onClick={locationOpenHandler}
        disabled={selectedLocation === undefined}
      >
        <LocationOnIcon />
      </IconButton>
      <IconButton onClick={salesmenOpenHandler}>
        <PersonIcon />
      </IconButton>
      <Equipment onSelected={equipmentSelectedHandler} />
      <ClientsDialog open={open} onClose={clientsDialogCloseHandler} />
      <DateRangeDialog
        open={dateRangeOpen}
        onClose={dateRangeDialogCloseHandler}
      />
      <TransportDialog open={transportOpen} onClose={transportCloseHandler} />

      {accessoriesOpen && (
        <AccessoriesDialog
          open={accessoriesOpen}
          onClose={accessoriesCloseHandler}
          callback={confirmationOpenHandler}
          onAllSelected={openRentalConirmationHandler}
        />
      )}
      <ConfirmationDialog
        open={confirmationOpen}
        onClose={confirmationCloseHandler}
      />
      <LocationDialog open={locationOpen} onClose={locationCloseHandler} />
      <SalesmenDialog open={salesmenOpen} onClose={salesmenCloseHandler} />
    </div>
  );
}

export default App;
