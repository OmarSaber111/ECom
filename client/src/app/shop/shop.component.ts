import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { ICategory } from '../shared/models/category';
import { productparam } from '../shared/models/productparam';
@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})

export class ShopComponent implements OnInit {
  productparam = new productparam();
  products:IProduct[]=[];
  categories:ICategory[]=[];
  totalCount!: number
constructor(private _Shopservice:ShopService){}
ngOnInit(): void {
  this.getallproducts();
  this.getallcategories();
  };
getallproducts(){
  this._Shopservice.getallproducts(this.productparam).subscribe({
    next: (response) => {
      this.products = response.data;
      this.totalCount = response.totalCount;
      this.productparam.pageNumber = response.pageNumber;
      this.productparam.pageSize = response.pageSize;
    },
    error: (error) => {
      console.error('Error fetching products:', error);
    }
  })
}

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
  
  filterbycategory(id: number) {
    this.productparam.categoryId = id;
    this.getallproducts();
  }
  sortoptions=[
    {name:'Price',value:'Name'},
    {name:'Price: Min to Max',value:'PriceAce'},
    {name:'Price: Max to Min',value:'PriceDce'}
  ]
   
  sortingbyprice(sort:Event){
   this.productparam.selectedSortOption = (sort.target as HTMLSelectElement).value;
   console.log(this.productparam.selectedSortOption);
   this.getallproducts();
  }

  
  onsearch(searchTerm: string) 
  {
    this.productparam.searchTerm = searchTerm;
      this.getallproducts(); 
    }


    resetevalue()
    {
      this.productparam.searchTerm = '';
      this.productparam.categoryId = 0;
      this.productparam.selectedSortOption = '';
      this.getallproducts();
    }
    onPageChanged(event: any) {
      this.productparam.pageNumber = event.page;
      this.productparam.pageSize = event.itemsPerPage;
      this.getallproducts();
    }
  }

