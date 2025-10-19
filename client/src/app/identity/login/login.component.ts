import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent  implements OnInit {
    getCookie(name: string): string | null {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop()!.split(';').shift() || null;
    return null;
  }
  formGroup! : FormGroup;
  emailModel: string = ''
    constructor(private _fb:FormBuilder,private _identityservice:IdentityService, private _toastr:ToastrService, private router:Router) { }
  ngOnInit(): void {
      this.formValidation();
  }
    formValidation() {
      this.formGroup = this._fb.group({
        email: ['',[Validators.required,Validators.email]],
        password: ['',[Validators.required,Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!%*?&])[A-Za-z\d!%*?&]{8,}$/)]]
      });
    }
    get _email(){
      return this.formGroup?.get('email');
    }
    get _password(){
      return this.formGroup?.get('password');
    }
    Submit()
    {
      if(this.formGroup.valid)
      {
        this._identityservice.login(this.formGroup.value).subscribe({
          next: (response) => {
            console.log(response);
            this._toastr.success('Login successful', 'Success');
            const token = this.getCookie('token');
            if (token) {
              sessionStorage.setItem('token', token);
            }
            this.router.navigateByUrl('/');
          },
          error: (error: any) => {
            console.log(error);
            this._toastr.error()
          }
        });
      }
    }
    SendEmailForgetpassword(){
      this._identityservice.forgetpassword(this.emailModel).subscribe({
        next(value) {
          console.log(value)
          
    
        },
        error(err) {
          console.log(err)
        },
      })

    }
}
