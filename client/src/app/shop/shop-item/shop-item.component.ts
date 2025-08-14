import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrl: './shop-item.component.scss'
})
export class ShopItemComponent  {
 @Input() product!: IProduct;
 constructor(private _basketservice: BasketService) { }
  setBasketItem() {
    this._basketservice.addItemToBasket(this.product)
  }

}