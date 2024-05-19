using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<IEnumerable<ProductType>> GetAllAsync();
        Task<ProductType> GetByIdAsync(int id);
        Task AddAsync(ProductType productType);
        Task UpdateAsync(ProductType productType);
        Task DeleteAsync(int id);
    }
}
