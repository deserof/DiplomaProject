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
import AddProductDialog from '../Dialogs/AddProductDialod';
import EditProductDialog from '../Dialogs/EditProductDialog';

const Products: React.FC = () => {
  const [totalCount, setTotalCount] = useState(0);
  const [pageNumber, setPage] = useState(0);
  const [pageSize, setPageSize] = useState(10);
  const [products, setProducts] = useState<Product[]>([]);
  const [addDialogOpen, setAddDialogOpen] = useState(false);
  const [editDialogOpen, setEditDialogOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);

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

  return (
    <Container>
      <Typography variant="h4" component="h1">
        Список продуктов
      </Typography>
      <Box mt={2}>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setAddDialogOpen(true)}
        >
          Добавить продукт
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
              <TableCell>Статус качества</TableCell>
              <TableCell>Действия</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {products.map((product) => (
              <TableRow key={product.id}>
                <TableCell>{product.name}</TableCell>
                <TableCell>{product.description}</TableCell>
                <TableCell>{product.qualityStatus}</TableCell>
                <TableCell>
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