import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ActiveAccount } from '../shared/models/activeaccount';
import { resetepassword } from '../shared/models/resetepassword';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private _http:HttpClient) { }
  baseurl = environment.baseUrl
  register(form: any) {
    return this._http.post(this.baseurl+"Auth/Register", form);
  }
  active(param:ActiveAccount)
  {
    return this._http.post(this.baseurl+"Auth/active-account",param);
  }
  login(form:any)
  {
    return this._http.post(this.baseurl+"Auth/LogIn",form);
  }
  forgetpassword(email:string){
    return this._http.get(this.baseurl+`Auth/send-email-forget-password?email=${email}`)
  }
  resetePassword(form:resetepassword){
    return this._http.post(this.baseurl+"Auth/resete-password",form);
  }
}
