using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Services.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetFilteredProductsAsync(int? productTypeId, bool? inStock, string sortOrder);
        Task UpdateAsync(Product product);
    }
}