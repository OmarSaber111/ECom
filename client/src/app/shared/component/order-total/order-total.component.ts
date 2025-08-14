import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../../basket/basket.service';
import { IBasketTotal } from '../../models/basket';

@Component({
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrls: ['./order-total.component.scss']
})
export class OrderTotalComponent implements OnInit {
  basketTotals: IBasketTotal | null = null;

  constructor(private _basketservice: BasketService) { }

  ngOnInit(): void {
    this._basketservice.baskeTotal$.subscribe({
      next: (total) => {
        this.basketTotals = total;
        console.log("fffff",total)
      },
      error: (err) => {
        console.log('Error fetching basket total:', err);
      }
    });
  }
}
