import React, { useState, useEffect } from "react";
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
  
  interface EditDetailDialogProps {
    open: boolean;
    onClose: () => void;
    editedDetail: Detail | null;
  }
  
  const EditDetailDialog: React.FC<EditDetailDialogProps> = ({
    open,
    onClose,
    editedDetail,
  }) => {
    const [details, setDetails] = useState<Detail[]>([]);
    const [name, setName] = useState("");
  
    useEffect(() => {
      if (editedDetail) {
        setName(editedDetail.name);
      }
    }, [editedDetail]);
  
    const handleSave = () => {
        if (editedDetail) {
          fetch(`http://localhost:7115/api/details`, {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              Id: editedDetail.id,
              Name: name,
            }),
          })
            .then((response) => {
              if (!response.ok) {
                throw new Error("Failed to update detail");
              }
              onClose();
              return response.json();
            })
            .catch((error) => console.error("Error updating detail:", error));
        }
      };
  
    return (
      <Dialog open={open} onClose={onClose}>
        <DialogTitle>Edit Detail</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Please enter the name of the edited detail.
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
          <Button onClick={handleSave} color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>
    );
  };
  
  export default EditDetailDialog;