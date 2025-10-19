import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/models/basket';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'], // make sure it's 'styleUrls', not 'styleUrl'
})
export class NavBarComponent implements OnInit {
  cartCount!: Observable<IBasket | null>;
  visibale: boolean = false;

  constructor(
    private _basketservice: BasketService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {
    // ✅ Browser-only code
    if (isPlatformBrowser(this.platformId)) {
      const basketid = localStorage.getItem('basketId');
      if (basketid) {
        this._basketservice.GetBasket(basketid).subscribe({
          next: (basket) => {
            if (basket) {
              console.log('Basket loaded successfully:', basket);
              this.cartCount = this._basketservice.basket$;
            } else {
              console.error('No basket found for the given ID');
            }
          },
          error: (err) => {
            console.error('Error loading basket:', err);
          },
        });
      }
    }
  }
  hoveredItem: string | null = null;
  ToggleDropDown() {
    this.visibale = !this.visibale;
  }
  closeDropDown() {
  this.visibale = false;
  }
}
