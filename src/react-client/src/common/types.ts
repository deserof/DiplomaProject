export interface Product {
    id: number;
    name: string;
    description: string;
    creationDate: string;
    qualityStatus: string;
  }
  
  export interface ProductsResponse {
    items: Product[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
  }

  export interface User{
    id: number;
    firstName: string;
    lastName: string;
    position: string;
    hireDate: Date;
  }

  export interface UsersResponse {
    items: User[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
  }

  export interface ProductionProcess {
    id: number;
    name: string;
    description: string;
  }
  
  export interface ProductionProcessesResponse {
    items: ProductionProcess[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
  }