import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from '../../shared/models/product';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit{
constructor(private _shopservice:ShopService, private _route:ActivatedRoute, private _toastr:ToastrService) { }
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
}

