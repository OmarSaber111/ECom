import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from '../../shared/models/product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit{
constructor(private _shopservice:ShopService, private _route:ActivatedRoute) { }
  ngOnInit(): void {
    this.getProductDetails();
  }

 productDetails!: IProduct;

  getProductDetails() {
    this._shopservice.getproductdetails(Number(this._route.snapshot.paramMap.get('id'))).subscribe({
      next: (response) => {
        this.productDetails = response;
      },
      error: (error) => {
        console.error('Error fetching product details:', error);
      }
    });
  }
  }


