import React, { useEffect, useState } from 'react';
import MainMenu from '../Menu/MainMenu';
import { Box, Button, Container, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TablePagination, TableRow, Typography } from '@mui/material';
import { ProductionProcess } from '../../common/types';
import { addProductionProcess, deleteProductionProcess, getProductionProcesses, updateProductionProcess } from '../../common/apiService';
import { useNavigate } from 'react-router-dom';
import { Add, Delete, Edit } from '@mui/icons-material';
import EditProductionProcessDialog from '../Dialogs/EditProductionProcessDialog';
import AddProductionProcessDialog from '../Dialogs/AddProductionProcessDialog';

const ProductionProcesses: React.FC = () => {
  const [totalCount, setTotalCount] = useState(0);
  const [pageNumber, setPage] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [products, setProducts] = useState<ProductionProcess[]>([]);
  const [addDialogOpen, setAddDialogOpen] = useState(false);
  const [editDialogOpen, setEditDialogOpen] = useState(false);
  const [selectedProductionProcess, setSelectedProductionProcess] = useState<ProductionProcess | null>(null);
  const [search, setSearch] = useState<string>('');
  const navigate = useNavigate();

  useEffect(() => {
    console.log('Текущая страница:', pageNumber);
  }, [pageNumber]);
  
  useEffect(() => {
    fetchProductionProcesses();
  }, [pageNumber]);
  
  useEffect(() => {
    fetchProductionProcesses();
  }, []);

  const fetchProductionProcesses = async () => {
    const data = await getProductionProcesses(pageNumber + 1, pageSize);
    setProducts(data.items);
    setTotalCount(data.totalCount);
  };

  const handleAddProductionProcess = async (productionProcess: ProductionProcess) => {
    await addProductionProcess(productionProcess);
    fetchProductionProcesses();
    setAddDialogOpen(false);
  };

  const handleUpdateProductionProcess = async (productionProcess: ProductionProcess) => {
    await updateProductionProcess(productionProcess);
    fetchProductionProcesses();
    setEditDialogOpen(false);
  };

  const handleDeleteProductionProcess = async (id: number) => {
    await deleteProductionProcess(id);
    fetchProductionProcesses();
  };

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value);
  };

    return (
<Container>
        <MainMenu />
      <Typography variant="h4" component="h1">
        Производственные процессы
      </Typography>
      <Box mt={2}>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setAddDialogOpen(true)}
        >
          Добавить производственный процесс
        </Button>

        <TableContainer component={Paper} sx={{ marginTop: 2 }}>
            <TablePagination
             component="div"
            count={totalCount}
            page={pageNumber}
            onPageChange={(event, newPage) => setPage(newPage)}
            rowsPerPage={pageSize}
            onRowsPerPageChange={(event) => {
             setPageSize(parseInt(event.target.value, 10));
            setPage(0);
             }}
             />
        </TableContainer>

      </Box>
      <TableContainer component={Paper} sx={{ marginTop: 2 }}>
        <Table>

          <TableHead>
            <TableRow>
              <TableCell>Название</TableCell>
              <TableCell>Описание</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
  {Array.isArray(products) &&
    products
      .filter((productionProcess) =>
        productionProcess.name.toLowerCase().includes(search.toLowerCase())
      )
      .map((productionProcess) => (
        <TableRow key={productionProcess.id}>
          <TableCell>{productionProcess.name}</TableCell>
          <TableCell>{productionProcess.description}</TableCell>
          <TableCell style={{ whiteSpace: 'nowrap' }}>
            <IconButton
              onClick={() => {
                setSelectedProductionProcess(productionProcess);
                setEditDialogOpen(true);
              }}
            >
              <Edit />
            </IconButton>
            <IconButton
              onClick={() => handleDeleteProductionProcess(productionProcess.id)}
            >
              <Delete />
            </IconButton>
          </TableCell>
        </TableRow>
      ))}
</TableBody>
        </Table>
      </TableContainer>
      <AddProductionProcessDialog
        open={addDialogOpen}
        onClose={() => setAddDialogOpen(false)}
        onSubmit={handleAddProductionProcess}
      />
      <EditProductionProcessDialog
        open={editDialogOpen}
        onClose={() => setEditDialogOpen(false)}
        onSubmit={handleUpdateProductionProcess}
        productionProcess={selectedProductionProcess}
      />
    </Container>
    );        
}

export default ProductionProcesses;