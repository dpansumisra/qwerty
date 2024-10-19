import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5182'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl + '/Product');
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl + '/Product', product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl + '/Product'}/${id}`);
  }

  // Add this to the product.service.ts
    private cart: Product[] = [];

    addToCart(product: Product): void {
      console.log('Product added to cart:', product);
      this.cart.push(product);
    }

    getCartItems(): Product[] {
      console.log("value check karne ", this.cart)
      return this.cart;
    }

}
