// AddProductDialog.tsx
import React, { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  Button,
} from '@mui/material';
import { Product } from '../../common/types';

interface AddProductDialogProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (product: Product) => void;
}

const AddProductDialog: React.FC<AddProductDialogProps> = ({
  open,
  onClose,
  onSubmit,
}) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [qualityStatus, setQualityStatus] = useState('');

  const handleSubmit = () => {
    const newProduct: Product = {
      name,
      description,
      qualityStatus,
      creationDate: new Date().toISOString(),
      id: 0
    };

    onSubmit(newProduct);

    setName('');
    setDescription('');
    setQualityStatus('');
  }

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Добавить изделие</DialogTitle>
      <DialogContent>
        <TextField
          autoFocus
          margin="dense"
          label="Название"
          fullWidth
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <TextField
          margin="dense"
          label="Описание"
          fullWidth
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <TextField
          margin="dense"
          label="Статус качества"
          fullWidth
          value={qualityStatus}
          onChange={(e) => setQualityStatus(e.target.value)}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Отмена</Button>
        <Button onClick={handleSubmit}>Отправить</Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddProductDialog;