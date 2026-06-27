import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class HeaderComponent {
  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  get isAuthenticated() {
    return this.authService.getIsAuthenticated();
  }

  navigateToHome() {
    this.router.navigate(['/home']);
  }

  navigateToDashboard() {
    this.router.navigate(['/dashboard']);
  }

  navigateToOrderHistory() {
    this.router.navigate(['/order-history']);
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  navigateToSignup() {
    this.router.navigate(['/signup']);
  }

  logout() {
    localStorage.removeItem('Mytoken');
    this.authService.setAuthenticated(false);
    this.router.navigate(['/home']);
  }
}
