using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(int customerId, Dictionary<int, int> orders);
        Task DeleteAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetFullOrder(int id);
        Task<IEnumerable<Order>> GetOrdersByCustomerAndDateRangeAsync(int customerId, DateTime startDate, DateTime endDate);
    }
}