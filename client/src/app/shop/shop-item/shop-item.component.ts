import { Component, Input } from '@angular/core';
import { IProduct } from '../../shared/models/product';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrl: './shop-item.component.scss'
})
export class ShopItemComponent {
 @Input() product!: IProduct;
}
