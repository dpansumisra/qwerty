// src/app/models/cart-item.model.ts
import { Product } from "./product";
export interface CartItem {
    cartItemID: number;
    product: Product;  // Make sure to import the Product model
    quantity: number;
}
