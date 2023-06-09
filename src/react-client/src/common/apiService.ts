import axios from 'axios';
import { BASE_URL } from './constants';
import { ProcessesResponse, Product, ProductionProcess, ProductionProcessesResponse, ProductsResponse, UsersResponse } from './types';

function getAccessToken() {
  return localStorage.getItem('access_token');
}

const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use(
  (config) => {
    const token = getAccessToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Products

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

export const getProduct = async (id: number): Promise<Product> => {
  const response = await api.get(`/api/Products/${id}`);
  return response.data;
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

// Users

export const getUsers = async (pageNumber: number, pageSize: number): Promise<UsersResponse> => {
  try {
    const response = await api.get('/api/Users', {
      params: {
        pageNumber,
        pageSize,
      },
    });

    return response.data;
  } catch (error) {
    console.error('Ошибка при получении пользователей:', error);
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

// Production processes

export const getProductionProcesses = async (pageNumber: number, pageSize: number): Promise<ProductionProcessesResponse> => {
  try {
    const response = await api.get('/api/ProductionProcesses', {
      params: {
        pageNumber,
        pageSize,
      },
    });

    return response.data;
  } catch (error) {
    console.error('Ошибка при получении процессов производства:', error);
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

export const addProductionProcess = async (productionProcess: ProductionProcess) => {
  const response = await api.post<Product>('/api/ProductionProcesses', productionProcess);
  return response.data;
};

export const updateProductionProcess = async (productionProcess: ProductionProcess) => {
  const response = await api.put<Product>(`/api/ProductionProcesses/${productionProcess.id}`, productionProcess);
  return response.data;
};

export const deleteProductionProcess = async (id: number) => {
  const response = await api.delete(`/api/ProductionProcesses/${id}`);
  return response.data;
};

// Processes

export const getProcesses = async (productId: number, pageNumber: number, pageSize: number): Promise<ProcessesResponse> => {
  try {
    const response = await api.get('/api/Processes', {
      params: {
        productId,
        pageNumber,
        pageSize,
      },
    });

    return response.data;
  } catch (error) {
    console.error('Ошибка при получении процессов изделия:', error);
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

export const uploadProcessPhoto = async (id: number, file: File) => {
  const formData = new FormData();
  formData.append('processPhoto', file);

  const response = await api.put(`/api/Processes/${id}/photo`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response.data;
};

export const uploadProcessFile = async (id: number, file: File) => {
  const formData = new FormData();
  formData.append('processFile', file);

  const response = await api.put(`/api/Processes/${id}/file`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response.data;
};

