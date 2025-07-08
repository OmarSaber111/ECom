import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})

export class ShopComponent implements OnInit {
products:IProduct[]=[];
constructor(private _Shopservice:ShopService){}
ngOnInit(): void {
  this.getallproducts();
  };
getallproducts(){
  this._Shopservice.getallproducts().subscribe({
    next: (response) => {
      this.products = response.data;
    },
    error: (error) => {
      console.error('Error fetching products:', error);
    }
  })
}

}
