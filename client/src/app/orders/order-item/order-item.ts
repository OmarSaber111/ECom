import { Component, OnInit } from '@angular/core';
import { IOrder } from '../../shared/models/order';
import { ActivatedRoute} from '@angular/router';
import { Oreders } from '../oreders';

@Component({
  selector: 'app-order-item',
  standalone: false,
  templateUrl: './order-item.html',
  styleUrl: './order-item.scss'
})
export class OrderItem  implements OnInit {
  
  order! :IOrder;
  id:number = 0;
  constructor(private _router:ActivatedRoute, private _service : Oreders) { }
  ngOnInit(): void {
    this._router.queryParams.subscribe(params => {
      this.id = params['id'] || 0;
    });
    this._service.getOrders(this.id).subscribe({
      next:(value)=>{
        this.order=value;
        console.log('fffffffffffffffffffffffff',this.order.orderItems);
      },
      error:(error)=>{
        console.log('xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx',error);
      }
    })
  }

}
