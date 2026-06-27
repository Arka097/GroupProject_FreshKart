import { Component, signal, ChangeDetectionStrategy } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CustomerService } from '../customer.service';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './login.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './login.css',
})
export class Login {
  loginForm: FormGroup;
  isLoading = signal(false);
  successMessage = signal('');
  errorMessage = signal('');

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private authService: AuthService,
    private router: Router,
  ) {
    this.loginForm = this.fb.group({
      emailId: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading.set(true);// set to true because we are making a request to the backend for login using signalling
      this.successMessage.set('');
      this.errorMessage.set('');

      const { emailId, password } = this.loginForm.value;

      this.customerService.login(emailId, password).subscribe({
        next: (response) => {
          this.isLoading.set(false);
          console.log('Login response:', response);

          if (response.isSuccess) {
            this.successMessage.set('Login successful!');

            if (response.token) {
              localStorage.setItem('Mytoken', response.token);
            } else {
              localStorage.setItem('Mytoken', 'dummy-token');
            }
            this.authService.setAuthenticated(true);

            this.loginForm.reset();

            setTimeout(() => {
              this.router.navigate(['/dashboard']);
            }, 1500);
          } else {
            this.errorMessage.set(response.message || 'Login failed');
          }
        },
        error: (error) => {
          console.error('Login failed:', error);
          if (error.error && error.error.message) {
            this.errorMessage.set(error.error.message);
          } else {
            this.errorMessage.set('Login failed. Please check your credentials and ensure backend is running.');
          }
          this.isLoading.set(false);
        },
      });
    }
  }
}
