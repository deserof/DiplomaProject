// FileDialog.tsx
import React, { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from '@mui/material';
import { uploadProcessFile } from '../../common/apiService';

interface FileDialogProps {
  open: boolean;
  onClose: () => void;
  onAdd: (file: File | null, processId: number) => void;
  processId: number;
}

let selectedFile: File | null = null;

const FileDialog: React.FC<FileDialogProps> = ({ open, onClose, onAdd, processId }) => {



  return (
    <Dialog open={open} onClose={onClose} aria-labelledby="file-dialog-title">
      <DialogTitle id="file-dialog-title">Добавить файл</DialogTitle>
      <DialogContent>
        <input
          type="file"
          id="fileUpload"
          style={{ display: 'none' }}
          onChange={(e) => {
            selectedFile = e.target.files ? e.target.files[0] : null;
          }}
          accept=".dwg,.dxf,.cdw,.spw"
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
            if (selectedFile) {
              uploadProcessFile(processId, selectedFile).then(() => {
                onAdd(selectedFile, processId);
                onClose();
              });
            } else {
              alert('Пожалуйста, выберите файл');
            }
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