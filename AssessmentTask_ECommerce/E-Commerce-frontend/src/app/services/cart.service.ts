// src/app/services/cart.service.ts
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { CartItem } from '../models/cart-item.model';
import { Product } from '../models/product';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems: CartItem[] = [];

  constructor() {}

  // Simulating a fetch from the backend
  getCartItems(): Observable<CartItem[]> {
    return of(this.cartItems);
    
  }

  // Add a product to the cart
  addToCart(product: Product, quantity: number): Observable<void> {
    const existingItem = this.cartItems.find(item => item.product.productID === product.productID);
    
    if (existingItem) {
      existingItem.quantity += quantity;
    } else {
      this.cartItems.push({ cartItemID: this.cartItems.length + 1, product, quantity });
    }
    return of();
  }

  // Update cart item quantity
  updateCartItem(item: CartItem): Observable<void> {
    const index = this.cartItems.findIndex(cartItem => cartItem.cartItemID === item.cartItemID);
    if (index !== -1) {
      this.cartItems[index] = item;
    }
    return of();
  }

  // Remove a cart item
  removeCartItem(cartItemID: number): Observable<void> {
    this.cartItems = this.cartItems.filter(item => item.cartItemID !== cartItemID);
    return of();
  }
}
