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
import { addProcess, deleteProcess, getProcessPhotos, getProcesses, getProduct, uploadProcessPhoto } from '../../common/apiService';
import ExpandMore from '@mui/icons-material/ExpandMore';
import ExpandLess from '@mui/icons-material/ExpandLess';
import AddProcessDialog from '../Dialogs/AddProcessDialog';
import { formatDate } from '../../common/dateUtils';

const Processes: React.FC = () => {
  const [fileDialogOpen, setFileDialogOpen] = useState<boolean>(false);
  const openFileDialog = (processId: number) => {
    setCurrentProcessId(processId);
    setFileDialogOpen(true);
  };
  const [currentProcessId, setCurrentProcessId] = useState<number | null>(null);
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
  const [images, setImages] = useState([]);

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

  const handleDeleteProcess = async (processId: number) => {
    await deleteProcess(processId);
    fetchProcesses();
  };

  const [processImages, setProcessImages] = useState<{ [key: number]: any[] }>({});
  const handleDetailsClick = async (processId: number) => {
    if (selectedProcess !== processId) {
      await fetchPhotos(processId);
    }
    setSelectedProcess(selectedProcess === processId ? null : processId);
  };
  const fetchPhotos = async (processId: number) => {
    const fetchedImages = await getProcessPhotos(processId);
    setProcessImages((prevImages) => ({
      ...prevImages,
      [processId]: fetchedImages,
    }));
  };
  const [addDialogOpen, setAddDialogOpen] = useState(false);

  const handleAddProcess = async (process: Process) => {
    await addProcess(process);
    fetchProcesses();
    setAddDialogOpen(false);
  };

  return (
    <Container>
      <MainMenu />
      <h1>Процессы для изделия {productName}</h1>
      <Button
        variant="contained"
        color="primary"
        style={{ marginRight: '1rem', marginBottom: '1rem' }}
        onClick={() => setAddDialogOpen(true)}
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
                  >
                    {selectedProcess === process.id ? <ExpandLess /> : <ExpandMore />}
                    <span style={{ marginLeft: '0.25rem' }}>Подробнее</span>
                  </div>

                  {selectedProcess === process.id && (
                    <div>
                      <h3>Расширенное описание процесса:</h3>
                      <p>{process.productionProcessDescription}</p>
                      <div>
                        {processImages[process.id]?.map((image, index) => (
                          <img
                            key={index}
                            src={`data:image/png;base64, ${image.imageData}`}
                            alt={`Процесс фото ${index + 1}`}
                            style={{ maxWidth: '100%', maxHeight: '300px', margin: '0.5rem' }}
                          />
                        ))}
                      </div>
                    </div>
                  )}
                </TableCell>
                <TableCell>{formatDate(process.startTime)}</TableCell>
                <TableCell>{formatDate(process.endTime)}</TableCell>
                <TableCell>
                  <IconButton onClick={() => openDeleteDialog(process.id)}>
                    <Delete />
                  </IconButton>
                  <IconButton onClick={() => openFileDialog(process.id)}>
                    <CloudUpload />
                  </IconButton>
                  <IconButton
                    onClick={() => {
                      const photoUploadInput = document.getElementById(
                        `photoUpload-${process.id}`
                      ) as HTMLInputElement;
                      photoUploadInput?.click();
                    }}
                  >
                    <AddAPhoto />
                  </IconButton>

                  <input
                    type="file"
                    id={`photoUpload-${process.id}`}
                    accept="image/*"
                    style={{ display: 'none' }}
                    onChange={async (e) => {
                      if (e.target.files && e.target.files.length > 0) {
                        const file = e.target.files[0];
                        try {
                          console.log('Добавлено фото:', file, 'для процесса с ID:', process.id);
                          await uploadProcessPhoto(process.id, file);
                          console.log('Фото успешно загружено');
                        } catch (error) {
                          console.error('Ошибка при загрузке фото:', error);
                        }
                      }
                    }}
                  />

                  <FileDialog
                    open={fileDialogOpen}
                    onClose={closeFileDialog}
                    processId={currentProcessId}
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

      <AddProcessDialog
        open={addDialogOpen}
        onClose={() => setAddDialogOpen(false)}
        onSubmit={handleAddProcess}
        productId={Number(id)}
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

    </Container>
  );
}
export default Processes;