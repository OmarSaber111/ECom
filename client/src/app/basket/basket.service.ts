import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotal } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService  {

  constructor(private _http :HttpClient) { }

  BaseUrl = 'https://localhost:44375/api/CustomerBasket/';
   private basketsource = new BehaviorSubject<IBasket | null>(null);
  basket$ = this.basketsource.asObservable();
  private basketSourseTotal = new BehaviorSubject<IBasketTotal | null>(null);
  baskeTotal$ = this.basketSourseTotal.asObservable();
  shipPrice:number = 0;

    calcualateTotal() {
    const basket = this.GetCurrentValue();
    if (!basket) {
      return;
    }
    const shipping = this.shipPrice;
    const subTotal = basket.basketItems.reduce((a, c) => {
      return (c.price * c.quantity) + a
    }, 0)
    const total = shipping + subTotal
    this.basketSourseTotal.next({ shipping, subTotal, total })
  }



  
  
  GetBasket(Id: string) {
    return this._http.get<IBasket>(this.BaseUrl + "Get-Customer-Basket/" + Id).pipe(
      map((value: IBasket) => {
        this.basketsource.next(value)
        this.calcualateTotal();
        return value
      })
    )
  }
  SetBasket(basket: IBasket) {
    return this._http.post<IBasket>(this.BaseUrl + "Add-Customer-Basket ", basket).subscribe(
      (value: IBasket) => {
        this.basketsource.next(value);
        this.calcualateTotal();
      },
      (err) => {
        console.log(err);
      }
    );
  }
  GetCurrentValue() {
    return this.basketsource.value
  }
 
 
  addItemToBasket(product: IProduct, quantity: number = 1) {
    const itemToAdd: IBasketItem = this.MapPrpductToBasketItem(product, quantity);
    debugger
    let basket = this.GetCurrentValue()
    console.log(basket)
     
    if (!basket||basket?.id=='null') {
     
      basket = this.CreateBasket();
    }
 
    basket.basketItems = this.AddOrUpdate(basket.basketItems, itemToAdd, quantity);
    return this.SetBasket(basket)
  }
  private AddOrUpdate(basketItem: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = basketItem.findIndex(i => i.id === itemToAdd.id);
    if (index == -1) {
      itemToAdd.quantity = quantity;
      basketItem.push(itemToAdd)
    }
    else {
      basketItem[index].quantity += quantity;
    }
    return basketItem;
  }
 
  private CreateBasket(): IBasket {
    const basket = new Basket();
    console.log(basket)
    localStorage.setItem('basketId', basket.id);
    return basket;
  }
  MapPrpductToBasketItem(product: IProduct, quantity: number): IBasketItem {
    return {
      id: product.id,
      category: product.categoryName,
      img: product.photos[0].imgName,
      name: product.name,
      price: product.newPrice,
      quantity: quantity,
      description: product.description
    }
  }
 
  incrementBasketItemQuantity(item: IBasketItem) {
    const basket = this.GetCurrentValue();
    if (!basket || !basket.basketItems) {
      return;
    }
    const itemIndex = basket.basketItems.findIndex((i) => i.id === item.id);
    basket.basketItems[itemIndex].quantity++;
    this.SetBasket(basket);
  }
 
  DecrementBasketItemQuantity(item: IBasketItem) {
    const basket = this.GetCurrentValue();
    if (!basket || !basket.basketItems) {
      return;
    }
    const itemIndex = basket.basketItems.findIndex((i) => i.id === item.id);
    if (basket.basketItems[itemIndex].quantity > 1) {
      basket.basketItems[itemIndex].quantity--;
      this.SetBasket(basket);
    } else {
      this.removeItemFormBasket(item);
    }
  }
  removeItemFormBasket(item: IBasketItem) {
    const basket = this.GetCurrentValue();
    if (!basket || !basket.basketItems) {
      return;
    }
    if (basket.basketItems.some((i) => i.id === item.id)) {
      basket.basketItems = basket.basketItems.filter((i) => i.id !== item.id);
      if (basket.basketItems.length > 0) {
        this.SetBasket(basket);
      } else {
        this.DeleteBaskeItem(basket);
      }
    }
  }
  DeleteBaskeItem(basket: IBasket) {
    return this._http
      .delete(this.BaseUrl + 'Delete-Customer-Basket/' + basket.id)
      .subscribe({
        next: (value) => {
          this.basketsource.next(null);
          localStorage.removeItem('basketId');
        },
        error(err) {
          console.log(err);
        },
      });
  }
 
}