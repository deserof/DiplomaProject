import axios from 'axios';
import { BASE_URL } from './constants';
import { Product, ProductsResponse } from './types';

const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getProducts = async (pageNumber: number, pageSize: number): Promise<ProductsResponse> => {
    try {
      const response = await api.get('/api/Products', {
        params: {
          pageNumber,
          pageSize,
        },
      });
  
      return response.data;
    } catch (error) {
      console.error('Ошибка при получении продуктов:', error);
      return {
        items: [],
        pageNumber: 0,
        totalPages: 0,
        totalCount: 0,
        hasPreviousPage: false,
        hasNextPage: false,
      };
    }
  };

export const addProduct = async (product: Product) => {
  const response = await api.post<Product>('/api/Products', product);
  return response.data;
};

export const updateProduct = async (product: Product) => {
  const response = await api.put<Product>(`/api/Products/${product.id}`, product);
  return response.data;
};

export const deleteProduct = async (id: number) => {
  const response = await api.delete(`/api/Products/${id}`);
  return response.data;
};