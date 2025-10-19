import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from '../../basket/basket.service';
import { ToastrService } from 'ngx-toastr';
import { CheckoutService } from '../checkout.service';
import { IBasket } from '../../shared/models/basket';
import { ICreateorder } from '../../shared/models/order';
import { Router } from '@angular/router';


@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss'
})
export class Payment implements OnInit{
  @Input() delivery!: FormGroup;
  @Input() address!: FormGroup;
  ngOnInit(): void {
  
  }
  
  constructor(private _basket:BasketService, private _toastr:ToastrService, private _checkout:CheckoutService, private router: Router){}
  CreateOrder(){
    const basket=this._basket.GetCurrentValue();
    const order = this.GetOrderToCreate(basket);
    this._checkout.createOrder(order).subscribe({
      next:(value)=>{
        this._toastr.success("Order Created Successfully","Success");
        this._basket.DeleteBasket();
        this.router.navigate(['/checkout/success'], { queryParams: { orderId: value.id } });

      },
      error:(error)=>{
         console.error("Validation Errors:", error.error.errors);
        console.log(error);
      }
    })
  }
  GetOrderToCreate(basket: IBasket | null) : ICreateorder{
    return {
      basketId: basket?.id!,
      deliveryMethodId: +this.delivery.value.delivery,
      shippingAddressDto: this.address.value
    }
  }
  }



