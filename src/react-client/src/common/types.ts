export interface PaginatedResponse<T> {
  items: T[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface Product {
  id: number;
  name: string;
  description: string;
  creationDate: string;
  qualityStatus: string;
}

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  position: string;
  hireDate: Date;
}

export interface ProductionProcess {
  id: number;
  name: string;
  description: string;
}

// Processes

export interface Process {
  id: number;
  name: string;
  description: string;
  startTime: string;
  endTime: string;
  productionProcessId: number;
  productionProcessDescription: string;
  productId: number;
}
