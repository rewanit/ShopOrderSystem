using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Repositories.Interfaces
{
    public interface IProductTypeRepository : IGenericRepository<ProductType, int>
    {
    }
}