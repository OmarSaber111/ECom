import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
fromGroup!: FormGroup; 

  constructor(private fb: FormBuilder, private _identityservice: IdentityService,private _tostar:ToastrService, private router:Router) {}

  ngOnInit(): void {
    this.formValidation();
  }

  formValidation() {
    this.fromGroup = this.fb.group({
      UserName: ['', [Validators.required, Validators.minLength(6)]],
      email: ['', [Validators.required, Validators.email]],
      DisplayName: ['', [Validators.required]],
      password: ['', [
        Validators.required,
        Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!%*?&])[A-Za-z\d!%*?&]{8,}$/)
      ]]
    });
  }
  get _username(){
    return this.fromGroup?.get('UserName');
  }
  get _email(){
    return this.fromGroup?.get('email');
  }
  get _displayName(){
    return this.fromGroup?.get('DisplayName');
  }
  get _password(){
    return this.fromGroup?.get('password');
  }
  Submit()
  {
    if(this.fromGroup.valid)
    {
      this._identityservice.register(this.fromGroup.value).subscribe({
        next: (response) => {
          console.log(response);
          this._tostar.success('Registration successful, Please Confirm your email', 'Success');
          this.router.navigateByUrl('/Account/login');
        },
        error: (error: any) => {
          console.log(error);
          this._tostar.error(error.error.statusMessage, 'Error');
        }
      });
    }

  }

}
