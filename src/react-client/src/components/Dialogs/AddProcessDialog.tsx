import React, { useState } from 'react';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
} from '@mui/material';
import { Process, ProductionProcess } from '../../common/types';

interface AddProcessDialogProps {
    open: boolean;
    onClose: () => void;
    onSubmit: (productionProcess: Process) => void;
}

const AddProcessDialog: React.FC<AddProcessDialogProps> = ({
    open,
    onClose,
    onSubmit,
}) => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = () => {
        const newProcess: Process = {
            name,
            description,
            id: 0,
            startTime: '',
            endTime: '',
            productionProcessId: 0,
            productionProcessDescription: ''
        };

        onSubmit(newProcess);

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

export default AddProcessDialog;