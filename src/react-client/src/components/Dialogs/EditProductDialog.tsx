// EditProductDialog.tsx
import React, { useState, useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  Button,
} from '@mui/material';
import { Product } from '../../common/types';

interface EditProductDialogProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (product: Product) => void;
  product: Product | null;
}

const EditProductDialog: React.FC<EditProductDialogProps> = ({
  open,
  onClose,
  onSubmit,
  product,
}) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [qualityStatus, setQualityStatus] = useState('');

  useEffect(() => {
    if (product) {
      setName(product.name);
      setDescription(product.description);
      setQualityStatus(product.qualityStatus);
    }
  }, [product]);

  const handleSubmit = () => {
    if (product) {
      onSubmit({ ...product, name, description, qualityStatus });
    }
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Изменить продукт</DialogTitle>
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
        <Button onClick={handleSubmit}>Сохранить</Button>
      </DialogActions>
    </Dialog>
  );
};

export default EditProductDialog;