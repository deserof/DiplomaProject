import React, { useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  Button,
} from '@mui/material';
import { ProductionProcess } from '../../common/types';

interface AddProductionProcessDialogProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (productionProcess: ProductionProcess) => void;
}

const AddProductionProcessDialog: React.FC<AddProductionProcessDialogProps> = ({
  open,
  onClose,
  onSubmit,
}) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  const handleSubmit = () => {
    const newProductionProcess: ProductionProcess = {
      name,
      description,
      id: 0
    };

    onSubmit(newProductionProcess);

    setName('');
    setDescription('');
  }

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Добавить производственный процесс</DialogTitle>
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
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Отмена</Button>
        <Button onClick={handleSubmit}>Отправить</Button>
      </DialogActions>
    </Dialog>
  );
};

export default AddProductionProcessDialog;