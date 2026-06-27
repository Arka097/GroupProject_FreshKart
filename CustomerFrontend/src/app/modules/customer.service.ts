import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private apiUrl = 'http://localhost:3000/api/Customer';

  constructor(private http: HttpClient) {}

  login(emailId: string, password: string): Observable<any> {
    const body = { emailId, password };
    return this.http.post<any>(`${this.apiUrl}/login`, body);
  }

  signup(emailId: string, password: string): Observable<any> {
    const body = { emailId, password };
    return this.http.post<any>(`${this.apiUrl}/signup`, body);
  }

  getStockItems(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetStockItems`);
  }

  placeOrder(order: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/PlaceOrder`, order);
  }

  getOrderHistory(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetOrderHistory`);
  }
}
