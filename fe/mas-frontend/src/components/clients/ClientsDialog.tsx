import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import Tab from "@mui/material/Tab";
import Tabs from "@mui/material/Tabs";
import { useState } from "react";
import TabPanel from "./TabPanel";
import ExistingClientsTab from "./tabs/ExistingClientsTab";
import NewIndividualClientTab from "./tabs/NewIndividualClientTab";
import NewBusinessClientTab from "./tabs/NewBusinessClientTab";
import { Box } from "@mui/material";

interface ClientsDialog {
  open: boolean;
  onClose: () => void;
}

const ClientsDialog = ({ open, onClose }: ClientsDialog) => {
  const [tab, setTab] = useState(0);

  const tabChangeHandler = (event: React.SyntheticEvent, newTab: number) => {
    setTab(newTab);
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Wybierz klienta</DialogTitle>
      <DialogContent>
        <Box sx={{ borderBottom: 1, bordernColor: "divider" }}>
          <Tabs
            value={tab}
            onChange={tabChangeHandler}
            aria-label="client tabs"
          >
            <Tab label="Istniejący klienci" />
            <Tab label="Nowy klient peronalny" />
            <Tab label="Nowy klient biznesowy" />
          </Tabs>
        </Box>
        <TabPanel value={tab} index={0}>
          <ExistingClientsTab />
        </TabPanel>
        <TabPanel value={tab} index={1}>
          <NewIndividualClientTab onClose={() => setTab(0)} />
        </TabPanel>
        <TabPanel value={tab} index={2}>
          <NewBusinessClientTab onClose={() => setTab(0)} />
        </TabPanel>
      </DialogContent>
    </Dialog>
  );
};

export default ClientsDialog;
