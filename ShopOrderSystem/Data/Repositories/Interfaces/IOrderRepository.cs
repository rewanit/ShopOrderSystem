using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
        Task<Order> GetFullAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByCustomerAndDateRangeAsync(int customerId, DateTime startDate, DateTime endDate);
    }
}