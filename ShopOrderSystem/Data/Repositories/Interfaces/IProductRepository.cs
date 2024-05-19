using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        Task<IEnumerable<Product>> GetAllAsync(params int[] ids);
        Task<IEnumerable<Product>> GetFilteredProductsAsync(int? productTypeId, bool? inStock, string sortOrder);
        Task<IEnumerable<Product>> UnavailableProducts(Dictionary<int, int> orderItems);
        Task UpdateProductsQuantity(Dictionary<int, int> productIdQuantityMap);
    }
}