using Microsoft.Extensions.Caching.Memory;
using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Repositories
{
    public class OrderItemDetailRepository : GenericRepository<OrderItemDetail, int>, IOrderItemDetailRepository
    {
        public OrderItemDetailRepository(ShopOrderSystemDbContext context, IMemoryCache memoryCache) : base(context, memoryCache)
        {
        }


    }

}
