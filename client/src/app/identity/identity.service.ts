import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private _http:HttpClient) { }
  baseurl = environment.baseUrl
  register(form: any) {
    return this._http.post(this.baseurl+"Auth/Register", form);
  }
}
