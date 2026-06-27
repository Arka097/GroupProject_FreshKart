import { Routes } from '@angular/router';
import { Home } from './modules/home/home';
import { Login } from './modules/login/login';
import { Signup } from './modules/signup/signup';
import { Dashboard } from './modules/dashboard/dashboard';
import { OrderHistory } from './modules/order-history/order-history';
import { authGuard } from './modules/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'login', component: Login },
  { path: 'signup', component: Signup },
  { path: 'dashboard', component: Dashboard },
  { path: 'order-history', component: OrderHistory }
];
