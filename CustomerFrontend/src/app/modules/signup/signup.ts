import { Component, signal, ChangeDetectionStrategy } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CustomerService } from '../customer.service';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './signup.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './signup.css',
})
export class Signup {
  signupForm: FormGroup;
  isLoading = signal(false);
  successMessage = signal('');
  errorMessage = signal('');

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private authService: AuthService,
    private router: Router,
  ) {
    this.signupForm = this.fb.group({
      emailId: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password')?.value === g.get('confirmPassword')?.value
      ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.signupForm.valid) {
      this.isLoading.set(true);
      this.successMessage.set('');
      this.errorMessage.set('');

      const { emailId, password } = this.signupForm.value;

      this.customerService.signup(emailId, password).subscribe({
        next: (response) => {
          this.isLoading.set(false);
          if (response.isSuccess) {
            this.successMessage.set('Sign up successful!');
            console.log('Signup successful:', response);

            if (response.token) {
              localStorage.setItem('Mytoken', response.token);
            } else {
              localStorage.setItem('Mytoken', 'dummy-token');
            }
            this.authService.setAuthenticated(true);

            this.signupForm.reset();

            setTimeout(() => {
              this.router.navigate(['/dashboard']);
            }, 1500);
          } else {
            this.errorMessage.set(response.message || 'Signup failed');
            this.isLoading.set(false);
          }
        },
        error: (error) => {
          console.error('Signup failed:', error);
          if (error.error && error.error.message) {
            this.errorMessage.set(error.error.message);
          } else {
            this.errorMessage.set('Signup failed. Please ensure backend is running.');
          }
          this.isLoading.set(false);
        },
      });
    }
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }
}
