import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent implements OnInit {
  cartCount!: Observable<IBasket | null>;
  constructor(private _basketservice:BasketService) { }
  ngOnInit(): void {
    const basketid = localStorage.getItem('basketId');
    this._basketservice.GetBasket(basketid!).subscribe({
      next: (basket) => {
        if (basket) {
          console.log('Basket loaded successfully:', basket);
          this.cartCount = this._basketservice.basket$;
        } else {
          console.error('No basket found for the given ID');
        }
      }

  });
  
}
}
