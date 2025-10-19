import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { delivery } from '../shared/delivery';
import { ICreateorder, IOrder } from '../shared/models/order';


@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private _http : HttpClient) { }
  baseurl = environment.baseUrl
  updateAddress(address:any){
    return this._http.put(this.baseurl+"Auth/update-address",address);
  }
  getaddress(){
    return this._http.get(this.baseurl+"Auth/get-user-address");
  }
  getDeliveryMethods(){
    return this._http.get<delivery[]>(this.baseurl+"Orders/Get-DeliveryMethodsAsync");
  }
  createOrder(order:ICreateorder){
    return this._http.post<IOrder>(this.baseurl+"Orders/create-order",order);
  }
}
