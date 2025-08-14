import { v4 as uuidv4 } from 'uuid';
export interface IBasket {
  id: string
  basketItems: IBasketItem[]
}

export interface IBasketItem {
  id: number
  name: string
  img: string
  quantity: number
  price: number
  category: string
  description: string
}

export class Basket implements IBasket {
    id = uuidv4()
    basketItems: IBasketItem[] = []

  }


export interface IBasketTotal {
  shipping: number
  total: number
  subTotal: number
}