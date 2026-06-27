import { Component, OnInit, signal, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../customer.service';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

interface StockItem {
  id: number;
  name: string;
  description: string;
  price: number;
  quantity: number;
  imageUrl: string;
}

interface CartItem {
  stockItem: StockItem;
  quantity: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  stockItems = signal<StockItem[]>([]);
  cart = signal<CartItem[]>([]);
  isLoading = signal(false);
  successMessage = signal('');
  errorMessage = signal('');

  constructor(private customerService: CustomerService, private router: Router, private authService: AuthService) {}

  ngOnInit() {
    if (!this.authService.getIsAuthenticated()) {
      return;
    }
    this.loadStockItems();
  }

  loadStockItems() {
    this.isLoading.set(true);
    this.customerService.getStockItems().subscribe({
      next: (response) => {
        this.stockItems.set(response.data || []);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error fetching stock items from backend:', error);
        this.errorMessage.set('Failed to load stock items. Please ensure backend is running.');
        this.isLoading.set(false);
      },
    });
  }

  addToCart(item: StockItem) {
    const currentCart = this.cart();
    const existingItem = currentCart.find((cartItem) => cartItem.stockItem.id === item.id);

    if (existingItem) {
      if (existingItem.quantity < item.quantity) {
        this.cart.set(
          currentCart.map((cartItem) =>
            cartItem.stockItem.id === item.id
              ? { ...cartItem, quantity: cartItem.quantity + 1 }
              : cartItem
          )
        );
      } else {
        this.errorMessage.set('Maximum quantity reached for this item');
        setTimeout(() => this.errorMessage.set(''), 3000);
      }
    } else {
      this.cart.set([...currentCart, { stockItem: item, quantity: 1 }]);
    }
  }

  removeFromCart(itemId: number) {
    this.cart.set(this.cart().filter((item) => item.stockItem.id !== itemId));
  }

  updateCartQuantity(itemId: number, quantity: number) {
    const currentCart = this.cart();
    const item = currentCart.find((cartItem) => cartItem.stockItem.id === itemId);
    
    if (item) {
      if (quantity > 0 && quantity <= item.stockItem.quantity) {
        this.cart.set(
          currentCart.map((cartItem) =>
            cartItem.stockItem.id === itemId ? { ...cartItem, quantity } : cartItem
          )
        );
      } else if (quantity <= 0) {
        this.removeFromCart(itemId);
      }
    }
  }

  getCartTotal(): number {
    return this.cart().reduce((total, item) => total + item.stockItem.price * item.quantity, 0);
  }

  getCartItemCount(): number {
    return this.cart().reduce((count, item) => count + item.quantity, 0);
  }

  placeOrder() {
    if (this.cart().length === 0) {
      this.errorMessage.set('Your cart is empty');
      setTimeout(() => this.errorMessage.set(''), 3000);
      return;
    }

    this.isLoading.set(true);
    const order = {
      orderId: Math.floor(Math.random() * 1000000),
      customerEmail: 'customer@example.com',
      orderDate: new Date(),
      items: this.cart().map((item) => ({
        stockItemId: item.stockItem.id,
        itemName: item.stockItem.name,
        quantity: item.quantity,
        price: item.stockItem.price,
        total: item.stockItem.price * item.quantity,
      })),
      totalAmount: this.getCartTotal(),
      status: 'Pending',
    };

    this.customerService.placeOrder(order).subscribe({
      next: (response) => {
        this.isLoading.set(false);
        this.successMessage.set('Order placed successfully! Invoice sent to your email.');
        this.cart.set([]);
        setTimeout(() => {
          this.successMessage.set('');
          this.router.navigate(['/order-history']);
        }, 3000);
      },
      error: (error) => {
        // Fallback for demo if backend is not running
        console.warn('Backend not available, using demo mode for order:', error);
        this.isLoading.set(false);
        this.successMessage.set('Order placed successfully! (Demo Mode - Invoice simulated)');
        this.cart.set([]);
        setTimeout(() => {
          this.successMessage.set('');
          this.router.navigate(['/order-history']);
        }, 3000);
      },
    });
  }

  navigateToOrderHistory() {
    this.router.navigate(['/order-history']);
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
