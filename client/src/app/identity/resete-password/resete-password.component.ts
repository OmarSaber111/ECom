import { Component, OnInit } from '@angular/core';
import { resetepassword } from '../../shared/models/resetepassword';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';

@Component({
  selector: 'app-resete-password',
  templateUrl: './resete-password.component.html',
  styleUrls: ['./resete-password.component.scss']
})
export class ResetePasswordComponent implements OnInit {
  formgroup!: FormGroup;
  resetepasswordModel = new resetepassword();

  constructor(private _router: ActivatedRoute, private _fb: FormBuilder,private _service:IdentityService,private router:Router) {}

  ngOnInit(): void {
    this._router.queryParams.subscribe(params => {
      this.resetepasswordModel.email = params['email'];
      this.resetepasswordModel.token = params['code'];
    });
    this.formValidation();
  }

  formValidation() {
    this.formgroup = this._fb.group(
      {
        password: [
          '',
          [
            Validators.required,
            Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!%*?&])[A-Za-z\d!%*?&]{8,}$/)
          ]
        ],
        confirmPassword: [
          '',
          [
            Validators.required,
            Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!%*?&])[A-Za-z\d!%*?&]{8,}$/)
          ]
        ]
      },
      { validator: this.passwordMatch }
    );
  }

  get _password() {
    return this.formgroup?.get('password');
  }
  get _confirmPassword() {
    return this.formgroup?.get('confirmPassword');
  }

  // Validator for matching passwords
  passwordMatch(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;

    if (password !== confirmPassword) {
      formGroup.get('confirmPassword')?.setErrors({ mismatch: true });
    } else {
      const errors = formGroup.get('confirmPassword')?.errors;
      if (errors) {
        delete errors['mismatch'];
        if (Object.keys(errors).length === 0) {
          formGroup.get('confirmPassword')?.setErrors(null);
        } else {
          formGroup.get('confirmPassword')?.setErrors(errors);
        }
      }
    }
  }

  Submit() {
    if (this.formgroup.valid) {

      this.resetepasswordModel.password = this.formgroup.value.password;
      this._service.resetePassword(this.resetepasswordModel).subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigateByUrl('/Account/login');
        },
        error: (error: any) => {
          console.log(error.statusMessage);
        }
      });
      
    }
  }
}
