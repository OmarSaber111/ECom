import { IProduct } from "./product";

export interface Ipagination {
    pageNumber: number;
    pageSize:   number;
    totalCount: number;
    data:       IProduct[];
}