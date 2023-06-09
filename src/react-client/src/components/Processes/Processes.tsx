import { useNavigate } from 'react-router-dom';
import { Button, Container } from '@mui/material';
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
import { Process } from '../../common/types';
import { getProcesses, getProduct } from '../../common/apiService';
import ExpandMore from '@mui/icons-material/ExpandMore';
import ExpandLess from '@mui/icons-material/ExpandLess';

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
  const [processes, setProcesses] = useState<Process[]>([]);
  const [totalCount, setTotalCount] = useState(0);
  const [pageNumber, setPage] = useState(0);
  const [pageSize, setPageSize] = useState(1000);
  const [productName, setProductName] = useState<string>();

  useEffect(() => {
    console.log('Загрузка продукта и процессов');
    fetchProcesses();
    fetchGetProduct();
  }, []);

  const fetchProcesses = async () => {
    const data = await getProcesses(Number(id), pageNumber + 1, pageSize);
    setTotalCount(data.totalCount);
    setProcesses(data.items);
  };

  const fetchGetProduct = async () => {
    const data = await getProduct(Number(id));
    setProductName(data.name);
  }

  const [selectedProcess, setSelectedProcess] = useState<number | null>(null);
  const handleDetailsClick = (processId: number) => {
    setSelectedProcess(selectedProcess === processId ? null : processId);
  };

  const handleDeleteProcess = async (processId: number) => {
    // Здесь вызывайте функцию API для удаления процесса
    // await deleteProcess(processId);

    // Обновите список процессов после удаления
    fetchProcesses();
  };

  return (
    <Container>
      <MainMenu />
      <h1>Процессы для изделия {productName}</h1>
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
              <TableRow key={process.id}>
                <TableCell>
                  {process.name}
                </TableCell>
                <TableCell>
                  {process.description}

                  <div
                    onClick={() => handleDetailsClick(process.id)}
                    style={{
                      display: 'flex',
                      alignItems: 'center',
                      cursor: 'pointer',
                      marginTop: '0.5rem',
                      fontSize: '0.875rem',
                    }}
                  >      {selectedProcess === process.id ? <ExpandLess /> : <ExpandMore />}
                    <span style={{ marginLeft: '0.25rem' }}>Подробнее</span>
                  </div>

                  {selectedProcess === process.id && (
                    <div>
                      <h3>Расширенное описание процесса:</h3>
                      <p>{process.productionProcessDescription}</p>
                    </div>
                  )}
                </TableCell>
                <TableCell>{process.startTime}</TableCell>
                <TableCell>{process.endTime}</TableCell>
                <TableCell>
                  <TableCell>
                    <IconButton onClick={() => openDeleteDialog(process.id)}>
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
                    processId={process.id}
                    onAdd={(file, processId) => {
                      console.log('Добавлен файл:', file, 'для процесса с ID:', processId);
                    }}
                  />
                </TableCell>
              </TableRow>

            ))}
          </TableBody>
        </Table>
      </TableContainer>

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

    </Container>
  );
}
export default Processes;