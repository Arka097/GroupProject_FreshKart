using CustomerAPI.Model;

namespace CustomerAPI.Data.Repositories
{
    public interface IStockItemRepository
    {
        Task<List<StockItem>> GetAllAsync();
        Task<StockItem?> GetByIdAsync(int id);
        Task<StockItem> AddAsync(StockItem item);
        Task<StockItem> UpdateAsync(StockItem item);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
