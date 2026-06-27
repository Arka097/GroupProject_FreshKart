# Customer Application

A full-stack customer application with ASP.NET Core backend and Angular 22 frontend.

## Project Structure

```
d:/
├── CustomerApp-Backend/          # Backend Project Directory
│   └── CustomerAPI/              # ASP.NET Core Backend
│       ├── Controllers/          # API Controllers
│       ├── Business Layer/       # Business Logic
│       ├── Model/                # Data Models
│       ├── Common/               # Common Utilities
│       └── Program.cs            # Application Entry Point
└── CustomerFrontend/             # Angular 22 Frontend (Separate Project)
    ├── src/
    │   ├── app/
    │   │   ├── login/            # Login Component
    │   │   ├── dashboard/        # Dashboard with Cart
    │   │   ├── order-history/    # Order History Component
    │   │   ├── customer.service.ts
    │   │   ├── auth.service.ts
    │   │   └── ...
    │   └── ...
    └── package.json
```

## Features Implemented

### Backend (ASP.NET Core)
- ✅ JWT Authentication with cookie-based tokens
- ✅ Customer Login endpoint
- ✅ Static Stock Items data (8 items)
- ✅ Memory Caching for stock items
- ✅ Order Placement endpoint (simulated PDF invoice)
- ✅ Order History endpoint (last 5 orders)
- ✅ CORS configuration
- ✅ Encryption/Decryption utilities

### Frontend (Angular 22)
- ✅ Login page with form validation
- ✅ Dashboard with stock item grid
- ✅ Shopping cart functionality
- ✅ Add/remove items from cart
- ✅ Quantity adjustment
- ✅ Place order (skips payment gateway)
- ✅ Order history with expiry dates
- ✅ Notification status display
- ✅ Auth guard for protected routes
- ✅ JWT interceptor for API calls
- ✅ Responsive design with modern UI

## Setup Instructions

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd d:/CustomerApp-Backend/CustomerAPI
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Run the backend:
   ```bash
   dotnet run
   ```

The backend will run on `https://localhost:5000`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd d:/CustomerFrontend
   ```

2. Install npm dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

The frontend will run on `http://localhost:4200`

## Usage

1. **Login**: Use any email and password (min 6 characters) - authentication is simulated
2. **Dashboard**: Browse items, add to cart, adjust quantities
3. **Place Order**: Click "Place Order" to submit (simulates PDF invoice email)
4. **Order History**: View last 5 orders with expiry dates and notification status

## API Endpoints

- `POST /customer/login` - Customer login
- `GET /customer/GetStockItems` - Get stock items (authenticated)
- `POST /customer/PlaceOrder` - Place order (authenticated)
- `GET /customer/GetOrderHistory` - Get order history (authenticated)

## Notes

- The project follows the same pattern as `AngularJIS-JwtAuthentication-Latest` and `RestAPIDotNetCore-Stage-Latest`
- Stock data is static as requested
- PDF invoice generation is simulated (in production, integrate with a PDF library)
- Email sending is simulated (in production, integrate with SMTP service)
- JWT tokens are stored in cookies for security
- All protected routes require authentication via auth guard

## Configuration

Backend configuration is in `CustomerAPI/appsettings.json`:
- JWT settings (Key, Issuer, Audience)
- Cache settings
- CORS settings

Frontend API URL is configured in `CustomerFrontend/src/app/customer.service.ts`:
- Currently set to `https://localhost:5000/customer`
