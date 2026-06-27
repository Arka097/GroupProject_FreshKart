using CustomerAPI.Common;
using CustomerAPI.Model;
using CustomerAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Business_Layer
{
    public class BLLCustomer
    {
        private readonly ApplicationDbContext _context;

        public BLLCustomer(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch stock items from database
        public BllOutput GetStockItems()
        {
            try
            {
                var stockItems = _context.StockItems.ToList();

                return new BllOutput
                {
                    Data = stockItems,
                    IsSuccess = true,
                    ExtraData = null
                };
            }
            catch (Exception ex)
            {
                return new BllOutput
                {
                    Data = null,
                    IsSuccess = false,
                    ExtraData = ex.Message
                };
            }
        }

        public BllOutput Login(CustomerLogin login)
        {
            try
            {
                Console.WriteLine($"Login attempt for email: {login.EmailId}");
                Console.WriteLine($"Total customers in DB: {_context.Customers.Count()}");

                // Check if user exists
                var existingUser = _context.Customers.FirstOrDefault(c => c.EmailId == login.EmailId);
                if (existingUser == null)
                {
                    Console.WriteLine($"User not found: {login.EmailId}");
                    return new BllOutput
                    {
                        Data = null,
                        IsSuccess = false,
                        ExtraData = "Email not found. Please sign up first."
                    };
                }

                Console.WriteLine($"User found: {existingUser.EmailId}, CreatedAt: {existingUser.CreatedAt}");

                // Check if signup was within last 30 minutes
                var timeSinceSignup = DateTime.UtcNow - existingUser.CreatedAt;
                Console.WriteLine($"Time since signup: {timeSinceSignup.TotalMinutes} minutes");
                if (timeSinceSignup.TotalMinutes > 30)
                {
                    Console.WriteLine("Signup expired");
                    return new BllOutput
                    {
                        Data = null,
                        IsSuccess = false,
                        ExtraData = "Signup expired. Please sign up again."
                    };
                }

                // Re-encrypt password using stored encryption key and IV
                byte[] storedKey = Convert.FromBase64String(existingUser.EncryptionKey);
                byte[] storedIV = Convert.FromBase64String(existingUser.EncryptionIV);
                byte[] encrypted = EncryptDecrypt.Encrypt(login.Password, storedKey, storedIV);
                string encryptedBase64 = Convert.ToBase64String(encrypted);

                Console.WriteLine($"Stored encrypted pwd: {existingUser.EncryptedPwd}");
                Console.WriteLine($"Re-encrypted pwd: {encryptedBase64}");

                // Validate password
                if (existingUser.EncryptedPwd == encryptedBase64)
                {
                    Console.WriteLine("Password match - login successful");
                    return new BllOutput
                    {
                        Data = new { Email = login.EmailId },
                        IsSuccess = true,
                        ExtraData = null
                    };
                }

                Console.WriteLine("Password mismatch");
                return new BllOutput
                {
                    Data = null,
                    IsSuccess = false,
                    ExtraData = "Invalid password"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login exception: {ex.Message}");
                return new BllOutput
                {
                    Data = null,
                    IsSuccess = false,
                    ExtraData = ex.Message
                };
            }
        }

        public BllOutput Signup(CustomerLogin login)
        {
            try
            {
                // Check if user already exists
                var existingUser = _context.Customers.FirstOrDefault(c => c.EmailId == login.EmailId);
                if (existingUser != null)
                {
                    return new BllOutput
                    {
                        Data = null,
                        IsSuccess = false,
                        ExtraData = "User already exists"
                    };
                }

                // Create new user
                var newCustomer = new Customer
                {
                    EmailId = login.EmailId,
                    EncryptedPwd = login.EncryptedPwd,
                    EncryptionKey = login.EncryptionKey,
                    EncryptionIV = login.EncryptionIV,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Customers.Add(newCustomer);
                _context.SaveChanges();

                return new BllOutput
                {
                    Data = new { Email = login.EmailId },
                    IsSuccess = true,
                    ExtraData = null
                };
            }
            catch (Exception ex)
            {
                return new BllOutput
                {
                    Data = null,
                    IsSuccess = false,
                    ExtraData = ex.Message
                };
            }
        }

        public BllOutput PlaceOrder(Order order)
        {
            try
            {
                // In a real application, this would:
                // 1. Save order to database
                // 2. Generate PDF invoice
                // 3. Send email with PDF attachment
                // 4. Update stock quantities

                // For demo, just return success
                return new BllOutput
                {
                    Data = new { OrderId = order.OrderId, Message = "Order placed successfully. Invoice sent to email." },
                    IsSuccess = true,
                    ExtraData = null
                };
            }
            catch (Exception ex)
            {
                return new BllOutput
                {
                    Data = null,
                    IsSuccess = false,
                    ExtraData = ex.Message
                };
            }
        }

        public BllOutput GetOrderHistory(string customerEmail)
        {
            try
            {
                // Static order history data for demo
                var orderHistory = new List<OrderHistory>
                {
                    new OrderHistory
                    {
                        OrderId = 1001,
                        ItemName = "Basmati Rice (5kg)",
                        OrderDate = DateTime.Now.AddDays(-30),
                        ExpiryDate = DateTime.Now.AddDays(-15),
                        NotificationSent = true
                    },
                    new OrderHistory
                    {
                        OrderId = 1002,
                        ItemName = "Toor Dal (1kg)",
                        OrderDate = DateTime.Now.AddDays(-25),
                        ExpiryDate = DateTime.Now.AddDays(-10),
                        NotificationSent = true
                    },
                    new OrderHistory
                    {
                        OrderId = 1003,
                        ItemName = "Whole Wheat Atta (10kg)",
                        OrderDate = DateTime.Now.AddDays(-20),
                        ExpiryDate = DateTime.Now.AddDays(-5),
                        NotificationSent = true
                    },
                    new OrderHistory
                    {
                        OrderId = 1004,
                        ItemName = "Mustard Oil (1L)",
                        OrderDate = DateTime.Now.AddDays(-15),
                        ExpiryDate = DateTime.Now.AddDays(5),
                        NotificationSent = false
                    },
                    new OrderHistory
                    {
                        OrderId = 1005,
                        ItemName = "Turmeric Powder (200g)",
                        OrderDate = DateTime.Now.AddDays(-10),
                        ExpiryDate = DateTime.Now.AddDays(10),
                        NotificationSent = false
                    }
                };

                return new BllOutput
                {
                    Data = orderHistory.Take(5).ToList(),
                    IsSuccess = true,
                    ExtraData = null
                };
            }
            catch (Exception ex)
            {
                return new BllOutput
                {
                    Data = null,
                    IsSuccess = false,
                    ExtraData = ex.Message
                };
            }
        }
    }
}
