// src/app/shopping-cart/shopping-cart.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';  // Import FormsModule for ngModel
import { CartItem } from '../models/cart-item.model';  // Import CartItem model
import { Product } from '../models/product';     // Import Product model (if needed)
import { CartService } from '../services/cart.service';  // Import your cart service
import { DiscountService } from '../services/discount.service';  // Import your discount service

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],  // Include FormsModule here
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  cartItems: CartItem[] = [];
  discountCode: string = '';
  appliedDiscount: any = null;

  constructor(private cartService: CartService, private discountService: DiscountService) {}

  ngOnInit(): void {
    // Fetch the cart items from the service
    this.cartService.getCartItems().subscribe((items: CartItem[]) => {
      this.cartItems = items;
      console.log(items)
    });
  }

  getCartTotal(): number {
    return this.cartItems.reduce((total, item) => total + (item.product.price * item.quantity), 0);
  }

  getTotalAfterDiscount(): number {
    let total = this.getCartTotal();
    if (this.appliedDiscount) {
      total = total - (total * this.appliedDiscount.discountPercentage);
    }
    return total;
  }

  updateQuantity(item: CartItem): void {
    this.cartService.updateCartItem(item).subscribe(() => {
      console.log('Cart updated');
    });
  }

  removeFromCart(item: CartItem): void {
    this.cartService.removeCartItem(item.cartItemID).subscribe(() => {
      this.cartItems = this.cartItems.filter(cartItem => cartItem.cartItemID !== item.cartItemID);
    });
  }

  applyDiscount(): void {
    this.discountService.getDiscountByCode(this.discountCode).subscribe(
      (discount) => {
        this.appliedDiscount = discount;
        console.log(this.discountCode, discount, this.appliedDiscount)
      },
      (error) => {
        console.log(this.discountCode, this.appliedDiscount, error.message)
        alert('Invalid discount code');
      }
    );
  }
}
