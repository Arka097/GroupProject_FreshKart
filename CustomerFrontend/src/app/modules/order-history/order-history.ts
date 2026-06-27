import { Component, OnInit, signal, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../customer.service';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

interface OrderHistoryItem {
  orderId: number;
  itemName: string;
  orderDate: Date;
  expiryDate: Date;
  notificationSent: boolean;
}

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-history.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './order-history.css',
})
export class OrderHistory implements OnInit {
  orderHistory = signal<OrderHistoryItem[]>([]);
  isLoading = signal(false);
  errorMessage = signal('');

  constructor(private customerService: CustomerService, private router: Router, private authService: AuthService) {}

  ngOnInit() {
    if (!this.authService.getIsAuthenticated()) {
      return;
    }
    this.loadOrderHistory();
  }

  loadOrderHistory() {
    this.isLoading.set(true);
    this.customerService.getOrderHistory().subscribe({
      next: (response) => {
        this.orderHistory.set(response.data || []);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error fetching order history from backend:', error);
        this.errorMessage.set('Failed to load order history. Please ensure backend is running.');
        this.isLoading.set(false);
      },
    });
  }

  isExpiringSoon(expiryDate: Date): boolean {
    const today = new Date();
    const expiry = new Date(expiryDate);
    const diffTime = expiry.getTime() - today.getTime();
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    return diffDays <= 7 && diffDays > 0;
  }

  isExpired(expiryDate: Date): boolean {
    const today = new Date();
    const expiry = new Date(expiryDate);
    return expiry < today;
  }

  navigateToDashboard() {
    this.router.navigate(['/dashboard']);
  }

  logout() {
    localStorage.removeItem('Mytoken');
    this.authService.setAuthenticated(false);
    this.router.navigate(['/home']);
  }

  getIsAuthenticated() {
    return this.authService.getIsAuthenticated();
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }
}
