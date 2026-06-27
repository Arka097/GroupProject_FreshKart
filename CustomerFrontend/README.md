# Customer App Frontend

Angular 22 frontend for customer application with login, dashboard, and order history features.

## Features

- **Customer Login**: JWT-based authentication
- **Dashboard**: Browse stock items, add to cart, place orders
- **Order History**: View last 5 orders with expiry dates and notifications

## Setup

1. Install dependencies:
   ```bash
   npm install
   ```

2. Start the development server:
   ```bash
   npm start
   ```

3. Build for production:
   ```bash
   npm run build
   ```

## Backend API

The frontend connects to the ASP.NET Core backend running on `https://localhost:5000`.

## Project Structure

- `src/app/login` - Login component
- `src/app/dashboard` - Dashboard with cart functionality
- `src/app/order-history` - Order history component
- `src/app/customer.service.ts` - API service
- `src/app/auth.service.ts` - Authentication service

## Location

This project is located at `d:/CustomerFrontend/` as a separate project directory from the backend.
