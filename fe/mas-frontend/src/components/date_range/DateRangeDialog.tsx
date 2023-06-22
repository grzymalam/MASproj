import Dialog from "@mui/material/Dialog";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { useAppDispatch, useAppSelector } from "../../hooks/redux";
import { setEnd, setStart } from "../../store/durationSlice";
import { DateRangePicker } from "rsuite";
import "rsuite/dist/rsuite.min.css";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";
import { Box, Button, Typography } from "@mui/material";

interface DateRangeDialogProps {
  open: boolean;
  onClose: () => void;
}

const DateRangeDialog = ({ open, onClose }: DateRangeDialogProps) => {
  const dispatch = useAppDispatch();
  const start = useAppSelector((state) => state.durationReducer.start);
  const end = useAppSelector((state) => state.durationReducer.end);

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Wybierz zakres wypożyczenia</DialogTitle>
      <DialogContent>
        <Box sx={{ display: "flex" }}>
          <DatePicker
            sx={{ m: 1 }}
            label={"Od"}
            value={dayjs.unix(start)}
            onChange={(value) => {
              if (value!.unix() <= end) dispatch(setStart(value!.unix()));
            }}
            format="MM/DD/YYYY"
          />
          <DatePicker
            sx={{ m: 1 }}
            label={"Do"}
            value={dayjs.unix(end)}
            onChange={(value) => {
              if (value!.unix() >= start) dispatch(setEnd(value!.unix()));
            }}
            format="MM/DD/YYYY"
          />
        </Box>
        <Button onClick={onClose} variant="contained">
          Zatwierdź
        </Button>
      </DialogContent>
    </Dialog>
  );
};

export default DateRangeDialog;
