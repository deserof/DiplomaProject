import { useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';
import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    IconButton,
  } from '@mui/material';
  import { CloudUpload, Delete } from '@mui/icons-material';
  import { AddAPhoto } from '@mui/icons-material';
import FileDialog from '../Dialogs/FileDialog';
import DeleteDialog from '../Dialogs/DeleteDialog';
import MainMenu from '../Menu/MainMenu';
  
interface ProcessExecution {
  processExecutionId: number;
  startTime: string;
  endTime: string;
  productionProcessId: number;
  name: string;
  description: string;
}

interface ProcessesResponse {
  items: ProcessExecution[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

const Processes: React.FC = () => {
    const [fileDialogOpen, setFileDialogOpen] = useState<boolean>(false);

    const openFileDialog = () => {
      setFileDialogOpen(true);
    };
    
    const closeFileDialog = () => {
      setFileDialogOpen(false);
    };


  const [deleteDialogOpen, setDeleteDialogOpen] = useState<boolean>(false);
    const [processToDelete, setProcessToDelete] = useState<number | null>(null);
  
  const openDeleteDialog = (processId: number) => {
      setProcessToDelete(processId);
      setDeleteDialogOpen(true);
    };
  
  const closeDeleteDialog = () => {
      setDeleteDialogOpen(false);
    };

  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const [processes, setProcesses] = useState<ProcessExecution[]>([]);

  useEffect(() => {
    fetchProcesses();
  }, []);

  const fetchProcesses = async () => {
    // Здесь вызывайте функцию API для получения процессов
    // const data = await getProcesses(id);
    // setProcesses(data.items);

    // Заглушка для тестирования
    const data: ProcessesResponse = {
      items: [
        {
          processExecutionId: 1,
          startTime: '2021-01-01T08:00:00',
          endTime: '2021-01-01T09:00:00',
          productionProcessId: 1,
          name: 'Проверка качества сырья',
          description:
            'Проверка качества ткани, резиновых материалов, фитингов и т.д.',
        },
        {
            processExecutionId: 1,
            startTime: '2021-01-01T08:00:00',
            endTime: '2021-01-01T09:00:00',
            productionProcessId: 1,
            name: 'Проверка качества сырья',
            description:
              'Проверка качества ткани, резиновых материалов, фитингов и т.д.',
          },
          {
            processExecutionId: 1,
            startTime: '2021-01-01T08:00:00',
            endTime: '2021-01-01T09:00:00',
            productionProcessId: 1,
            name: 'Проверка качества сырья',
            description:
              'Проверка качества ткани, резиновых материалов, фитингов и т.д.',
          },
          {
            processExecutionId: 1,
            startTime: '2021-01-01T08:00:00',
            endTime: '2021-01-01T09:00:00',
            productionProcessId: 1,
            name: 'Проверка качества сырья',
            description:
              'Проверка качества ткани, резиновых материалов, фитингов и т.д.',
          },
      ],
      pageNumber: 1,
      totalPages: 1,
      totalCount: 1,
      hasPreviousPage: false,
      hasNextPage: false,
    };
    setProcesses(data.items);
  };

  const handleDeleteProcess = async (processId: number) => {
    // Здесь вызывайте функцию API для удаления процесса
    // await deleteProcess(processId);
  
    // Обновите список процессов после удаления
    fetchProcesses();
  };

  return (
    <div>
        <MainMenu />
<h1>Процессы для продукта {id}</h1>
    <Button
      variant="contained"
      color="primary"
      style={{ marginRight: '1rem', marginBottom: '1rem' }}
      onClick={() => {
        // Здесь добавьте логику для добавления процесса
      }}
    >
      Добавить процесс
    </Button>
    <Button
      variant="contained"
      color="secondary"
      style={{ marginBottom: '1rem' }}
      onClick={() => navigate('/products')}
    >
      Назад
    </Button>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Название</TableCell>
              <TableCell>Описание</TableCell>
              <TableCell>Время начала</TableCell>
              <TableCell>Время окончания</TableCell>
              <TableCell>Действия</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {processes.map((process) => (
              <TableRow key={process.processExecutionId}>
                <TableCell>{process.name}</TableCell>
                <TableCell>{process.description}</TableCell>
                <TableCell>{process.startTime}</TableCell>
                <TableCell>{process.endTime}</TableCell>
                <TableCell>
                <TableCell>
                <IconButton onClick={() => openDeleteDialog(process.processExecutionId)}>
                  <Delete />
                </IconButton>
                <IconButton onClick={() => openFileDialog()}>
                  <CloudUpload />
                </IconButton>
  <IconButton
  onClick={() => {
    const photoUploadInput = document.getElementById(
      'photoUpload'
    ) as HTMLInputElement;
    photoUploadInput?.click();
  }}
>
  <AddAPhoto />
  </IconButton>
</TableCell>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

<input
  type="file"
  id="photoUpload"
  accept="image/*"
  style={{ display: 'none' }}
  onChange={(e) => {
    // Здесь добавьте логику для загрузки фото
  }}
    />

<FileDialog
        open={fileDialogOpen}
        onClose={closeFileDialog}
        onAdd={(file) => {
          // Здесь добавьте логику для загрузки файла
          console.log('Добавлен файл:', file);
        }}
      />

<DeleteDialog
        open={deleteDialogOpen}
        onClose={closeDeleteDialog}
        onDelete={() => {
          if (processToDelete !== null) {
            handleDeleteProcess(processToDelete);
          }
          closeDeleteDialog();
        }}
      />

    </div>
  );
                }          
export default Processes;