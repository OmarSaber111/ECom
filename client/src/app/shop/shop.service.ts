import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ipagination } from '../shared/models/pagination';
import { ICategory } from '../shared/models/category';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  BaseUrl = 'https://localhost:44375/api/';
  constructor(private _Http: HttpClient) { }

  // Get All Products
  getallproducts(categoryId?:number, sort?:string, searchTerm?:string):Observable<Ipagination>
  {
    let params = new HttpParams();
    if(categoryId)
      {
        params = params.append('categoryId', categoryId);
      }
    if(sort)
      {
        params = params.append('sort', sort);
      }
    if(searchTerm)
      {
        params = params.append('search', searchTerm);
      }
    return this._Http.get<Ipagination>(this.BaseUrl + 'Product/Get-All-Product', { params });
  }

  // Get All Categories
  getallcategories():Observable<ICategory[]>
  {
    return this._Http.get<ICategory[]>(this.BaseUrl + 'Categories/Get-All-Category');
  }
}
