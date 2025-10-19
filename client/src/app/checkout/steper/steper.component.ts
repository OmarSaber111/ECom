import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-steper',
  templateUrl:'./steper.component.html',
  styleUrl:'./steper.component.scss'
})
export class SteperComponent {
 private _formBuilder = inject(FormBuilder);

  Address = this._formBuilder.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    street: ['', Validators.required],
    city: ['', Validators.required],
    zipCode: ['', Validators.required],
    state: ['', Validators.required],
  });
  DeliveryMethod = this._formBuilder.group({
    delivery: ['', Validators.required],
  });
}
