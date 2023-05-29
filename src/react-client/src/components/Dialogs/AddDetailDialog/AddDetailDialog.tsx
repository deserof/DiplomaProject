import React, { useState } from "react";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";

interface Detail {
    id: number;
    name: string;
  }

interface AddDetailDialogProps {
    open: boolean;
    onClose: () => void;
  }
  
  const AddDetailDialog: React.FC<AddDetailDialogProps> = ({
    open,
    onClose,
  }) => {
    const [name, setName] = useState("");
    const [details, setDetails] = useState<Detail[]>([]);

    const handleAdd = () => {
        fetch("http://localhost:7115/api/details", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ name }), // Отправьте только поле name
        })
          .then((response) => {
            if (!response.ok) {
              throw new Error("Failed to add detail");
            }
            return response.json();
          })
          .then((newDetail: Detail) => {
            // Добавьте новую деталь в состояние details
            setDetails([...details, newDetail]);
            onClose();
          })
          .catch((error) => console.error("Error adding detail:", error));
      };
  
    return (
      <Dialog open={open} onClose={onClose}>
        <DialogTitle>Add New Detail</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Please enter the name of the new detail.
          </DialogContentText>
          <TextField
            autoFocus
            margin="dense"
            id="name"
            label="Detail Name"
            type="text"
            fullWidth
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleAdd} color="primary">
            Add
          </Button>
        </DialogActions>
      </Dialog>
    );
  };
  
  export default AddDetailDialog;