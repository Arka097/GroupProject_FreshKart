# Customer API

ASP.NET Core backend API for customer application.

## Features

- **Customer Login**: JWT authentication with cookie-based tokens
- **Stock Items**: Static stock data with caching
- **Order Placement**: Place orders and generate PDF invoices (simulated)
- **Order History**: Retrieve last 5 orders with expiry tracking

## Setup

1. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

The API will run on `https://localhost:5000`.

## API Endpoints

### Authentication
- `POST /customer/login` - Customer login (returns JWT in cookie)

### Stock Items
- `GET /customer/GetStockItems` - Get all available stock items (requires authentication)

### Orders
- `POST /customer/PlaceOrder` - Place a new order (requires authentication)
- `GET /customer/GetOrderHistory` - Get last 5 orders (requires authentication)

## Configuration

JWT and cache settings are configured in `appsettings.json`.

## Project Structure

- `Controllers/CustomerController.cs` - API endpoints
- `Business Layer/BLLCustomer.cs` - Business logic
- `Model/` - Data models
- `Common/` - Common utilities (caching, entities)
