import { Gadget } from './Gadget';

export interface Pagination {
  items: Gadget[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
