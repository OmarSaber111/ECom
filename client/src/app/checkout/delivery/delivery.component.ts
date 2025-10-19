import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { delivery } from '../../shared/delivery';
import { CheckoutService } from '../checkout.service';
import { BasketService } from '../../basket/basket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrl: './delivery.component.scss'
})
export class Delivery implements OnInit {
  delivery : delivery[] = [];
  @Input() deliveryMethods!: FormGroup ;
  constructor(private _service:CheckoutService, private _basketservice:BasketService, private _toastre:ToastrService) { }
  ngOnInit(): void {
    this._service.getDeliveryMethods().subscribe({next:(res)=>{
      this.delivery = res;
      console.log(this.delivery);
    }})
    
  }
  createPayment(){
    const id =this.delivery.find(x => x.id === this.deliveryMethods.value.delivery)?.id;
    this._basketservice.createPaymentIntent(id).subscribe({
      next:res=>{
        this._toastre.success("Payment Intent Created Successfully","Success");
      },
      error:err=>{
        console.log(err);
      }
      
    })
  }
  setShippningPrice(){
    const delivery = this.delivery.find(x => x.id === this.deliveryMethods.value.delivery);
    if (delivery) {
      this._basketservice.setshippingprice(delivery);
    }
  }


}
