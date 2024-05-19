using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer,int>
    {
    }
}