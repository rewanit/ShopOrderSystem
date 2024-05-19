using Microsoft.Extensions.Caching.Memory;
using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(ShopOrderSystemDbContext context, IMemoryCache memoryCache) : base(context, memoryCache)
        {
        }
    }
}
