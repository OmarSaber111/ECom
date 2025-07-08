import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ipagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  BaseUrl = 'https://localhost:44375/api/Product/';
  constructor(private _Http: HttpClient) { }
  getallproducts():Observable<Ipagination>
  {
    return this._Http.get<Ipagination>(this.BaseUrl + 'Get-All-Product');
  }
}
