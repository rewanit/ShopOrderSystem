using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(ShopOrderSystemDbContext context, IMemoryCache memoryCache) : base(context, memoryCache)
        {
        }

        public async Task<Order> GetFullAsync(int id)
        {
            return await this.context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .ThenInclude(x=>x.OrderItemDetail)
                .FirstOrDefaultAsync(x => x.OrderId == id); ;
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerAndDateRangeAsync(int customerId, DateTime startDate, DateTime endDate)
        {
            return await context.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .OrderBy(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
