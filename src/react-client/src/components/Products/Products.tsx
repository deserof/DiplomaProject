import React, { useState, useEffect } from 'react';
import {
  Container,
  Typography,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  IconButton,
  Paper,
  Box,
  Button,
} from '@mui/material';
import { Add, Edit, Delete } from '@mui/icons-material';
import { TablePagination } from '@mui/material';
import { Product } from '../../common/types';
import { getProducts, addProduct, updateProduct, deleteProduct } from '../../common/apiService';
import AddProductDialog from '../Dialogs/AddProductDialog';
import EditProductDialog from '../Dialogs/EditProductDialog';
import { useNavigate } from 'react-router-dom';
import { TextField } from '@mui/material';
import MainMenu from '../Menu/MainMenu';

const Products: React.FC = () => {
  const [totalCount, setTotalCount] = useState(0);
  const [pageNumber, setPage] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [products, setProducts] = useState<Product[]>([]);
  const [addDialogOpen, setAddDialogOpen] = useState(false);
  const [editDialogOpen, setEditDialogOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [search, setSearch] = useState<string>('');
  const navigate = useNavigate();

  useEffect(() => {
    console.log('Текущая страница:', pageNumber);
  }, [pageNumber]);

  useEffect(() => {
    fetchProducts();
  }, [pageNumber]);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    const data = await getProducts(pageNumber + 1, pageSize);
    setProducts(data.items);
    setTotalCount(data.totalCount);
  };

  const handleAddProduct = async (product: Product) => {
    await addProduct(product);
    fetchProducts();
    setAddDialogOpen(false);
  };

  const handleUpdateProduct = async (product: Product) => {
    await updateProduct(product);
    fetchProducts();
    setEditDialogOpen(false);
  };

  const handleDeleteProduct = async (id: number) => {
    await deleteProduct(id);
    fetchProducts();
  };

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value);
  };

  return (
    <Container>
      <MainMenu />
      <Typography variant="h4" component="h1">
        Список изделий
      </Typography>
      <Box mt={2}>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setAddDialogOpen(true)}
        >
          Добавить изделие
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

        <TextField
          label="Поиск"
          value={search}
          onChange={handleSearchChange}
          variant="outlined"
          size="small"
          InputProps={{
            style: {
              borderRadius: '5px',
            },
          }}
          style={{ marginBottom: '1rem', width: '100%' }}
        />

      </Box>
      <TableContainer component={Paper} sx={{ marginTop: 2 }}>
        <Table>

          <TableHead>
            <TableRow>
              <TableCell>Название</TableCell>
              <TableCell>Описание</TableCell>
              <TableCell>Статус качества</TableCell>
              <TableCell>Дата добавления</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {Array.isArray(products) &&
              products
                .filter((product) =>
                  product.name.toLowerCase().includes(search.toLowerCase())
                )
                .map((product) => (
                  <TableRow key={product.id}>
                    <TableCell>{product.name}</TableCell>
                    <TableCell>{product.description}</TableCell>
                    <TableCell>{product.qualityStatus}</TableCell>
                    <TableCell>{product.creationDate}</TableCell>
                    <TableCell style={{ whiteSpace: 'nowrap' }}>
                      <IconButton
                        onClick={() => {
                          setSelectedProduct(product);
                          setEditDialogOpen(true);
                        }}
                      >
                        <Edit />
                      </IconButton>
                      <IconButton
                        onClick={() => handleDeleteProduct(product.id)}
                      >
                        <Delete />
                      </IconButton>
                      <Button
                        variant="outlined"
                        onClick={() => navigate(`/processes/${product.id}`)}
                      >
                        Процессы
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
          </TableBody>
        </Table>
      </TableContainer>
      <AddProductDialog
        open={addDialogOpen}
        onClose={() => setAddDialogOpen(false)}
        onSubmit={handleAddProduct}
      />
      <EditProductDialog
        open={editDialogOpen}
        onClose={() => setEditDialogOpen(false)}
        onSubmit={handleUpdateProduct}
        product={selectedProduct}
      />
    </Container>
  );
};

export default Products;