import { Component, OnInit } from '@angular/core';
import { IOrder, IOrderItem } from '../../shared/models/order';
import { Oreders } from '../oreders';


@Component({
  selector: 'app-order',
  standalone: false,
  templateUrl: './order.html',
  styleUrl: './order.scss'
})
export class Order implements OnInit {
orders:IOrder[]=[];
constructor(private _service:Oreders){} 
  ngOnInit(): void {
    this._service.getAllOrdersforauser().subscribe({
      next:(value)=>{
        this.orders=value;
        console.log('orders',this.orders);
      },
      error:(error)=>{
        console.log('error',error);
      }
    })
  }

getfirstimageorderitem(order:IOrderItem[]){
  return order.length>0?order[0].mainImg:null;
}
}
