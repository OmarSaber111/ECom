import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CheckoutRoutingModule } from './checkout-routing.module';
import { CheckoutComponent } from './checkout/checkout.component';
import { SteperComponent } from './steper/steper.component';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatStepperModule} from '@angular/material/stepper';



import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { AddressComponent } from './address/address.component';
import { Delivery } from './delivery/delivery.component';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { Payment } from './payment/payment.component';
import { Success } from './success/success.component';


@NgModule({
  declarations: [
    CheckoutComponent,
    SteperComponent,
    AddressComponent,
    Delivery,
    Payment,
  Success
  
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    MatButtonModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MatRadioModule
  
    
  ],
  
  exports:[
    SteperComponent,
    AddressComponent,
    Payment

  ]
})
export class CheckoutModule { }

