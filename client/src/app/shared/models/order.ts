export interface ICreateorder {
  deliveryMethodId: number
  basketId: string
  shippingAddressDto: IShippingAddress
}

export interface IShippingAddress {
  firstName: string
  lastName: string
  zipCode: string
  city: string
  street: string
  state: string
}

export interface IOrder {
  id: number;
  buyerEmail: string;
  subTotal: number;
  total: number;
  orderDate: string;
  shippingAddress: IShippingAddress;
  orderItems: IOrderItem[];
  deliveryMethod: string;
  status: string;
}

export interface IOrderItem {
  productId: number;
  mainImg: string;
  productName: string;
  price: number;
  quantity: number;
}