import React, { useEffect, useState } from 'react';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
} from '@mui/material';
import { Process, ProductionProcess } from '../../common/types';
import { Autocomplete } from '@mui/lab';
import { getProductionProcesses } from '../../common/apiService';

interface AddProcessDialogProps {
    open: boolean;
    onClose: () => void;
    onSubmit: (productionProcess: Process) => void;
    productId: number;
}

const formatDateTime = (dateString: string) => {
    const date = new Date(dateString);
    return date.toISOString();
};

const AddProcessDialog: React.FC<AddProcessDialogProps> = ({
    open,
    onClose,
    onSubmit,
    productId,
}) => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [startTime, setStartTime] = useState('');
    const [endTime, setEndTime] = useState('');
    const [productionProcesses, setProductionProcesses] = useState<ProductionProcess[]>([]);
    const [selectedProductionProcess, setSelectedProductionProcess] = useState<ProductionProcess | null>(null);

    useEffect(() => {
        const fetchProductionProcesses = async () => {
            const data = await getProductionProcesses(1, 1000);
            setProductionProcesses(data.items);
        };

        fetchProductionProcesses();
    }, []);

    const handleSubmit = () => {
        if (selectedProductionProcess) {
          const newProcess: Process = {
            name: selectedProductionProcess.name,
            description,
            id: 0,
            startTime: formatDateTime(startTime),
            endTime: formatDateTime(endTime),
            productionProcessId: selectedProductionProcess.id,
            productionProcessDescription: selectedProductionProcess.description,
            productId: productId,
          };
      
          onSubmit(newProcess);
      
          setName('');
          setDescription('');
          setStartTime('');
          setEndTime('');
          setSelectedProductionProcess(null);
        } else {
          alert('Пожалуйста, выберите процесс производства');
        }
      };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Добавить производственный процесс</DialogTitle>
            <DialogContent>
                <Autocomplete
                    options={productionProcesses}
                    getOptionLabel={(option) => option.name}
                    value={selectedProductionProcess}
                    onChange={(event, newValue) => {
                        setSelectedProductionProcess(newValue);
                    }}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            autoFocus
                            margin="dense"
                            label="Название"
                            fullWidth
                        />
                    )}
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
                    label="Время начала"
                    type="datetime-local"
                    fullWidth
                    InputLabelProps={{
                        shrink: true,
                    }}
                    value={startTime}
                    onChange={(e) => setStartTime(e.target.value)}
                />
                <TextField
                    margin="dense"
                    label="Время окончания"
                    type="datetime-local"
                    fullWidth
                    InputLabelProps={{
                        shrink: true,
                    }}
                    value={endTime}
                    onChange={(e) => setEndTime(e.target.value)}
                />
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>Отмена</Button>
                <Button onClick={handleSubmit}>Отправить</Button>
            </DialogActions>
        </Dialog>
    );
};

export default AddProcessDialog;