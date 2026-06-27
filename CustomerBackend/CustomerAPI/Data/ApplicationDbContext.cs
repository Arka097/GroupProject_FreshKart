using Microsoft.EntityFrameworkCore;
using CustomerAPI.Model;

namespace CustomerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure StockItem
            modelBuilder.Entity<StockItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
            });

            // Configure Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.CustomerEmail).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            });

            // Configure OrderHistory
            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.ItemName).IsRequired().HasMaxLength(200);
            });

            // Configure Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EmailId).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.EmailId).IsUnique();
            });

            // Seed Indian Groceries Data
            SeedStockItems(modelBuilder);
        }

        private void SeedStockItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockItem>().HasData(
                new StockItem { Id = 1, Name = "Basmati Rice (5kg)", Description = "Premium aged basmati rice - perfect for biryani and pulao", Price = 450m, Quantity = 50, ImageUrl = "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300&h=300&fit=crop" },
                new StockItem { Id = 2, Name = "Toor Dal (1kg)", Description = "Organic toor dal - high in protein", Price = 120m, Quantity = 200, ImageUrl = "https://images.unsplash.com/photo-1596484552834-6a58f850e0a3?w=300&h=300&fit=crop" },
                new StockItem { Id = 3, Name = "Whole Wheat Atta (10kg)", Description = "Chakki fresh wheat flour - perfect for roti and bread", Price = 380m, Quantity = 150, ImageUrl = "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=300&h=300&fit=crop" },
                new StockItem { Id = 4, Name = "Mustard Oil (1L)", Description = "Cold pressed mustard oil - traditional cooking oil", Price = 180m, Quantity = 75, ImageUrl = "https://images.unsplash.com/photo-1599940824399-b87987ced72a?w=300&h=300&fit=crop" },
                new StockItem { Id = 5, Name = "Turmeric Powder (200g)", Description = "Organic turmeric powder - 100% pure", Price = 85m, Quantity = 100, ImageUrl = "https://images.unsplash.com/photo-1615485500704-8e990f9900f7?w=300&h=300&fit=crop" },
                new StockItem { Id = 6, Name = "Red Chilli Powder (200g)", Description = "Kashmiri red chilli powder - adds color and flavor", Price = 95m, Quantity = 120, ImageUrl = "https://images.unsplash.com/photo-1569587112025-0d460e81a126?w=300&h=300&fit=crop" },
                new StockItem { Id = 7, Name = "Garam Masala (100g)", Description = "Authentic spice blend - essential Indian spice", Price = 110m, Quantity = 300, ImageUrl = "https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=300&h=300&fit=crop" },
                new StockItem { Id = 8, Name = "Jaggery (1kg)", Description = "Natural palm jaggery - unrefined cane sugar", Price = 140m, Quantity = 80, ImageUrl = "https://images.unsplash.com/photo-1621939514649-28b5fe7e8a3c?w=300&h=300&fit=crop" },
                new StockItem { Id = 9, Name = "Green Tea (250g)", Description = "Himalayan green tea - healthy and refreshing", Price = 320m, Quantity = 60, ImageUrl = "https://images.unsplash.com/photo-1556881286-fc6915169721?w=300&h=300&fit=crop" },
                new StockItem { Id = 10, Name = "Almonds (500g)", Description = "Premium California almonds - rich in nutrients", Price = 550m, Quantity = 90, ImageUrl = "https://images.unsplash.com/photo-1508061253366-f7da158b6d46?w=300&h=300&fit=crop" },
                new StockItem { Id = 11, Name = "Cashews (500g)", Description = "Premium cashew nuts - delicious and nutritious", Price = 480m, Quantity = 85, ImageUrl = "https://images.unsplash.com/photo-1533256207860-2f6e0083e9f0?w=300&h=300&fit=crop" },
                new StockItem { Id = 12, Name = "Poha (1kg)", Description = "Thin flattened rice - perfect for breakfast", Price = 75m, Quantity = 180, ImageUrl = "https://images.unsplash.com/photo-1585937421612-70a008356fbe?w=300&h=300&fit=crop" },
                new StockItem { Id = 13, Name = "Moong Dal (1kg)", Description = "Split green gram - versatile and nutritious", Price = 110m, Quantity = 160, ImageUrl = "https://images.unsplash.com/photo-1596484552834-6a58f850e0a3?w=300&h=300&fit=crop" },
                new StockItem { Id = 14, Name = "Masoor Dal (1kg)", Description = "Red lentils - quick cooking and protein-rich", Price = 100m, Quantity = 140, ImageUrl = "https://images.unsplash.com/photo-1596484552834-6a58f850e0a3?w=300&h=300&fit=crop" },
                new StockItem { Id = 15, Name = "Cumin Seeds (200g)", Description = "Organic cumin - aromatic spice for tempering", Price = 80m, Quantity = 130, ImageUrl = "https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=300&h=300&fit=crop" },
                new StockItem { Id = 16, Name = "Coriander Powder (200g)", Description = "Ground coriander - mild and citrusy flavor", Price = 70m, Quantity = 110, ImageUrl = "https://images.unsplash.com/photo-1599940824399-b87987ced72a?w=300&h=300&fit=crop" },
                new StockItem { Id = 17, Name = "Fenugreek Leaves (100g)", Description = "Dried methi leaves - aromatic herb", Price = 60m, Quantity = 95, ImageUrl = "https://images.unsplash.com/photo-1615485500704-8e990f9900f7?w=300&h=300&fit=crop" },
                new StockItem { Id = 18, Name = "Fennel Seeds (100g)", Description = "Saunf - sweet and aromatic seeds", Price = 65m, Quantity = 105, ImageUrl = "https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=300&h=300&fit=crop" },
                new StockItem { Id = 19, Name = "Coconut Oil (500ml)", Description = "Virgin coconut oil - cooking and health benefits", Price = 220m, Quantity = 70, ImageUrl = "https://images.unsplash.com/photo-1599940824399-b87987ced72a?w=300&h=300&fit=crop" },
                new StockItem { Id = 20, Name = "Sesame Oil (500ml)", Description = "Pure sesame oil - nutty flavor for seasoning", Price = 280m, Quantity = 55, ImageUrl = "https://images.unsplash.com/photo-1599940824399-b87987ced72a?w=300&h=300&fit=crop" },
                new StockItem { Id = 21, Name = "Besan (1kg)", Description = "Chickpea flour - for pakoras and sweets", Price = 110m, Quantity = 125, ImageUrl = "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=300&h=300&fit=crop" },
                new StockItem { Id = 22, Name = "Cornflour (500g)", Description = "Corn flour - for gravies and frying", Price = 55m, Quantity = 145, ImageUrl = "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=300&h=300&fit=crop" },
                new StockItem { Id = 23, Name = "Black Pepper (100g)", Description = "Freshly ground black pepper - pungent spice", Price = 120m, Quantity = 100, ImageUrl = "https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=300&h=300&fit=crop" },
                new StockItem { Id = 24, Name = "Cardamom Green (50g)", Description = "Whole green cardamom - aromatic and sweet", Price = 380m, Quantity = 40, ImageUrl = "https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=300&h=300&fit=crop" },
                new StockItem { Id = 25, Name = "Cinnamon Sticks (100g)", Description = "Ceylon cinnamon - warm and sweet spice", Price = 150m, Quantity = 75, ImageUrl = "https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=300&h=300&fit=crop" },
                new StockItem { Id = 26, Name = "Cloves (50g)", Description = "Whole cloves - strong aromatic spice", Price = 180m, Quantity = 60, ImageUrl = "https://images.unsplash.com/photo-1596040033229-a9821ebd058d?w=300&h=300&fit=crop" },
                new StockItem { Id = 27, Name = "Bay Leaves (50g)", Description = "Dried bay leaves - mild flavor for cooking", Price = 40m, Quantity = 120, ImageUrl = "https://images.unsplash.com/photo-1615485500704-8e990f9900f7?w=300&h=300&fit=crop" },
                new StockItem { Id = 28, Name = "Rock Salt (500g)", Description = "Natural rock salt - chemical-free", Price = 35m, Quantity = 200, ImageUrl = "https://images.unsplash.com/photo-1599940824399-b87987ced72a?w=300&h=300&fit=crop" },
                new StockItem { Id = 29, Name = "White Sugar (1kg)", Description = "Refined white sugar - pure and fine", Price = 45m, Quantity = 180, ImageUrl = "https://images.unsplash.com/photo-1621939514649-28b5fe7e8a3c?w=300&h=300&fit=crop" },
                new StockItem { Id = 30, Name = "Honey (500ml)", Description = "Pure raw honey - natural sweetener and medicine", Price = 350m, Quantity = 50, ImageUrl = "https://images.unsplash.com/photo-1587049352846-4a222e784d38?w=300&h=300&fit=crop" }
            );
        }
    }
}
