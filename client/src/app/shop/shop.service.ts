import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ipagination } from '../shared/models/pagination';
import { ICategory } from '../shared/models/category';
import { productparam } from '../shared/models/productparam';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  BaseUrl = 'https://localhost:44375/api/';
  constructor(private _Http: HttpClient) { }

  // Get All Products
  getallproducts(productparam:productparam):Observable<Ipagination>
  {
    let params = new HttpParams();
    if(productparam.categoryId)
      {
        params = params.append('categoryId', productparam.categoryId);
      }
    if(productparam.selectedSortOption)
      {
        params = params.append('sort', productparam.selectedSortOption);
      }
    if(productparam.searchTerm)
      {
        params = params.append('search', productparam.searchTerm);
      }
       params = params.append('pagenumber', productparam.pageNumber);
       params = params.append('pagesize', productparam.pageSize);
    return this._Http.get<Ipagination>(this.BaseUrl + 'Product/Get-All-Product', { params });
  }

  // Get All Categories
  getallcategories():Observable<ICategory[]>
  {
    return this._Http.get<ICategory[]>(this.BaseUrl + 'Categories/Get-All-Category');
  }
   getproductdetails(id: number): Observable<IProduct> {
  return this._Http.get<IProduct>(`${this.BaseUrl}Product/Get-Product-ById/${id}`);
}

}
