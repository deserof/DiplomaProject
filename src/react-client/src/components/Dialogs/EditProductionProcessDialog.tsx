import React, { useState, useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  Button,
} from '@mui/material';
import { ProductionProcess } from '../../common/types';

interface EditProductionProcessDialogProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (productionProcess: ProductionProcess) => void;
  productionProcess: ProductionProcess | null;
}

const EditProductionProcessDialog: React.FC<EditProductionProcessDialogProps> = ({
  open,
  onClose,
  onSubmit,
  productionProcess: productionProcess,
}) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [qualityStatus, setQualityStatus] = useState('');

  useEffect(() => {
    if (productionProcess) {
      setName(productionProcess.name);
      setDescription(productionProcess.description);
    }
  }, [productionProcess]);

  const handleSubmit = () => {
    if (productionProcess) {
      onSubmit({ ...productionProcess, name, description });
    }
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Изменить производственный процесс</DialogTitle>
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
        <Button onClick={handleSubmit}>Сохранить</Button>
      </DialogActions>
    </Dialog>
  );
};

export default EditProductionProcessDialog;