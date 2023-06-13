import axios from 'axios';
import { BASE_URL } from './constants';
import { PaginatedResponse, Process, Product, ProductionProcess, User } from './types';

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

export const getProducts = async (pageNumber: number, pageSize: number): Promise<PaginatedResponse<Product>> => {
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

export const getUsers = async (pageNumber: number, pageSize: number): Promise<PaginatedResponse<User>> => {
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

export const getProductionProcesses = async (pageNumber: number, pageSize: number): Promise<PaginatedResponse<ProductionProcess>> => {
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

export const addProcess = async (productionProcess: Process) => {
  const response = await api.post<Product>('/api/Processes', productionProcess);
  return response.data;
};

export const getProcesses = async (productId: number, pageNumber: number, pageSize: number): Promise<PaginatedResponse<Process>> => {
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

export const deleteProcess = async (id: number) => {
  const response = await api.delete(`/api/Processes/${id}`);
  return response.data;
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

export const uploadProcessFile = async (id: number | null, file: File) => {
  const formData = new FormData();
  formData.append('processFile', file);

  const response = await api.put(`/api/Processes/${id}/file`, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  });
  return response.data;
};

export const getProcessPhotos = async (processId: number) => {
  try {
    const response = await api.get(`/api/processes/${processId}/photos`);
    return response.data;
  } catch (error) {
    console.error('Ошибка при получении фотографий процесса:', error);
    return [];
  }
};

// report

export const downloadExcel = async (fromDate: Date, endDate: Date): Promise<Blob> => {
  try {
    const response = await api.get("/api/reports/generate", {
      responseType: "blob",
      params: {
        from: fromDate.toISOString(),
        end: endDate.toISOString(),
      },
    });

    return response.data;
  } catch (error) {
    console.error("Ошибка при загрузке Excel-файла:", error);
    throw error;
  }
};