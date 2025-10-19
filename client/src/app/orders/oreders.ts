import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IOrder } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class Oreders {
  constructor(private _http:HttpClient) { }
  baseurl = environment.baseUrl
  getOrders(id?:number){
    return this._http.get<IOrder>(this.baseurl+"Orders/Get-OrderByIdForUserAsync/"+id);
  }
  getAllOrdersforauser(){
    return this._http.get<IOrder[]>(this.baseurl+"Orders/Get-All-Orders-ForUserAsync");
  }
  
}
