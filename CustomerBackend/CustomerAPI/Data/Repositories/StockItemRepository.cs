using CustomerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly ApplicationDbContext _context;

        public StockItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockItem>> GetAllAsync()
        {
            return await _context.StockItems.ToListAsync();
        }

        public async Task<StockItem?> GetByIdAsync(int id)
        {
            return await _context.StockItems.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<StockItem> AddAsync(StockItem item)
        {
            await _context.StockItems.AddAsync(item);
            return item;
        }

        public async Task<StockItem> UpdateAsync(StockItem item)
        {
            _context.StockItems.Update(item);
            return await Task.FromResult(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item == null) return false;

            _context.StockItems.Remove(item);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
