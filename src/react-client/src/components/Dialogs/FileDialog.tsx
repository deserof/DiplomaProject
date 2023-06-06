// FileDialog.tsx
import React, { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from '@mui/material';

interface FileDialogProps {
  open: boolean;
  onClose: () => void;
  onAdd: (file: File | null) => void;
}

const FileDialog: React.FC<FileDialogProps> = ({ open, onClose, onAdd }) => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSelectedFile(e.target.files ? e.target.files[0] : null);
  };

  return (
    <Dialog open={open} onClose={onClose} aria-labelledby="file-dialog-title">
      <DialogTitle id="file-dialog-title">Добавить файл</DialogTitle>
      <DialogContent>
        <input
          type="file"
          id="fileUpload"
          style={{ display: 'none' }}
          onChange={handleFileChange}
        />
        <label htmlFor="fileUpload">
          <Button variant="outlined" color="primary" component="span">
            Выбрать файл
          </Button>
        </label>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Отмена
        </Button>
        <Button
          onClick={() => {
            onAdd(selectedFile);
            onClose();
          }}
          color="primary"
        >
          Добавить
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default FileDialog;