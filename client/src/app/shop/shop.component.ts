import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { ICategory } from '../shared/models/category';
import { Console } from 'console';

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
  this.getallcategories();
  };
getallproducts(){
  this._Shopservice.getallproducts(this.categoryId,this.selectedSortOption, this.searchTerm).subscribe({
    next: (response) => {
      this.products = response.data;
    },
    error: (error) => {
      console.error('Error fetching products:', error);
    }
  })
}
categories:ICategory[]=[];
getallcategories(){
  this._Shopservice.getallcategories().subscribe({
    next: (response) => {
      this.categories = response;
      console.log('Categories:', response);
    },
    error: (error) => {
      console.error('Error fetching categories:', error);
    }
  })}
  categoryId: number = 0;
  filterbycategory(id: number) {
    this.categoryId = id;
    this.getallproducts();
  }
  sortoptions=[
    {name:'Price',value:'Name'},
    {name:'Price: Min to Max',value:'PriceAce'},
    {name:'Price: Max to Min',value:'PriceDce'}
  ]
   selectedSortOption: string ='';
  sortingbyprice(sort:Event){
   this.selectedSortOption = (sort.target as HTMLSelectElement).value;
   console.log(this.selectedSortOption);
   this.getallproducts();
  }

  searchTerm: string = '';
  onsearch(searchTerm: string) 
  {
    this.searchTerm = searchTerm;
      this.getallproducts(); 
    }


    resetevalue()
    {
      this.searchTerm = '';
      this.categoryId = 0;
      this.selectedSortOption = '';
      this.getallproducts();
    }
  }

