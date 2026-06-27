using CustomerAPI.Model;

namespace CustomerAPI.Data.Repositories
{
    public interface IOrderHistoryRepository
    {
        Task<List<OrderHistory>> GetAllAsync();
        Task<OrderHistory?> GetByIdAsync(int id);
        Task<List<OrderHistory>> GetByCustomerEmailAsync(string email);
        Task<OrderHistory> AddAsync(OrderHistory history);
        Task<bool> SaveChangesAsync();
    }
}
