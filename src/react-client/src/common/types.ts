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