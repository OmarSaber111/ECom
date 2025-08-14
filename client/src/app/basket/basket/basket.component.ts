import { Component, OnInit } from '@angular/core';
import { BasketService } from '../basket.service';
import { IBasket, IBasketItem } from '../../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent implements OnInit {
  
  constructor(private _service: BasketService) {}

basket: IBasket | null = null;

ngOnInit(): void {
  this._service.basket$.subscribe({
    next: (value) => {
      this.basket = value;
      console.log('Basket updated:', this.basket);
    },
    error: (err) => {
      console.log(err);
    }
  });
}
RemoveBasket(item: IBasketItem) {
  this._service.removeItemFormBasket(item);
}

incrementQuantity(item: IBasketItem) {
  this._service.incrementBasketItemQuantity(item);
}

DecrementQuantity(item: IBasketItem) {
  this._service.DecrementBasketItemQuantity(item);
}


}
