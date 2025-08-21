import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from '../../shared/models/product';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit{
  quantity: number = 1;
constructor(private _shopservice:ShopService, private _route:ActivatedRoute, private _toastr:ToastrService, private _basketservice : BasketService) { }
  ngOnInit(): void {
    this.getProductDetails();
  }

 productDetails!: IProduct;
 mainimg!:string;
  getProductDetails() {
    this._shopservice.getproductdetails(Number(this._route.snapshot.paramMap.get('id'))).subscribe({
      next: (response) => {
        this.productDetails = response;
        this.mainimg = this.productDetails.photos[0].imgName;
        
        this._toastr.success('Product details loaded successfully!', 'Success');
      },
      error: (error) => {
        console.error('Error fetching product details:', error);
      }
    });
  }
  ReplaceImg(image: string) {
    this.mainimg = image;
  }
  incrementquantity() {
    if (this.quantity < 10) {
      this.quantity++;
      this._toastr.success('Quantity increased', 'Success');
    } else {
      this._toastr.warning('Maximum quantity reached', 'Enough');
    }
  }
  decrementquantity() {
    if (this.quantity > 1) {
      this.quantity--;
      this._toastr.success('Quantity decreased', 'Success');
    } else {
      this._toastr.warning('Minimum quantity is 1', 'Warning');
    }
  }
    addtobasket() {
      this._basketservice.addItemToBasket(this.productDetails, this.quantity)
}
calculatediscount( oldprice: number,newprice: number): number {
    return parseFloat(
     Math.round(((oldprice - newprice) / oldprice) * 100).toFixed(1)
     )
  }
}
