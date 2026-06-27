using CustomerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data.Repositories
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderHistory>> GetAllAsync()
        {
            return await _context.OrderHistories.ToListAsync();
        }

        public async Task<OrderHistory?> GetByIdAsync(int id)
        {
            return await _context.OrderHistories.FirstOrDefaultAsync(h => h.OrderId == id);
        }

        public async Task<List<OrderHistory>> GetByCustomerEmailAsync(string email)
        {
            return await _context.OrderHistories
                .Where(h => h.CustomerEmail == email)
                .ToListAsync();
        }

        public async Task<OrderHistory> AddAsync(OrderHistory history)
        {
            await _context.OrderHistories.AddAsync(history);
            return history;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
