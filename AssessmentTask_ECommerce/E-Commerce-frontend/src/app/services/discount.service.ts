import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Discount } from '../models/discount.model'; // Create this model for discount

@Injectable({
  providedIn: 'root'
})
export class DiscountService {
  private apiUrl = 'http://localhost:5182'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  // Fetch discount details by code
  getDiscountByCode(discountCode: string): Observable<Discount> {
    console.log('Fetching discount details by code: ', discountCode);
    return this.http.get<Discount>(`${this.apiUrl}/Product/Discount/${discountCode}`);
  }
}
