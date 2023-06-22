import {
  Box,
  Dialog,
  DialogContent,
  DialogTitle,
  Tab,
  Tabs,
} from "@mui/material";
import { useState } from "react";
import TabPanel from "../clients/TabPanel";
import FastTransportTab from "./FastTransportTab";
import SlowTransportTab from "./SlowTransportTab";

interface TransportDialogProps {
  open: boolean;
  onClose: () => void;
}

const TransportDialog = ({ open, onClose }: TransportDialogProps) => {
  const [tab, setTab] = useState(0);
  const tabChangeHandler = (event: React.SyntheticEvent, newTab: number) => {
    setTab(newTab);
  };
  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>
        Niestety sprzęt nie jest dostępny w twojej lokacji, wybierz tranport:
      </DialogTitle>
      <DialogContent>
        <Box sx={{ borderBottom: 1, bordernColor: "divider" }}>
          <Tabs
            value={tab}
            onChange={tabChangeHandler}
            aria-label="client tabs"
          >
            <Tab label="Szybki transport" />
            <Tab label="Wolny transport" />
          </Tabs>
        </Box>
        <TabPanel value={tab} index={0}>
          <FastTransportTab onClose={onClose} />
        </TabPanel>
        <TabPanel value={tab} index={1}>
          <SlowTransportTab onClose={onClose} />
        </TabPanel>
      </DialogContent>
    </Dialog>
  );
};

export default TransportDialog;
