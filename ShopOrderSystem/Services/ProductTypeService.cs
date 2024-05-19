using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Services.Interfaces;

namespace ShopOrderSystem.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository productTypeRepository;

        public ProductTypeService(IProductTypeRepository productTypeRepository)
        {
            this.productTypeRepository = productTypeRepository;
        }

        public async Task<IEnumerable<ProductType>> GetAllAsync()
        {
            return await productTypeRepository.GetAllAsync();
        }

        public async Task<ProductType> GetByIdAsync(int id)
        {
            return await productTypeRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(ProductType productType)
        {
            await productTypeRepository.AddAsync(productType);
            await productTypeRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductType productType)
        {
            productTypeRepository.Update(productType);
            await productTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productType = await productTypeRepository.GetByIdAsync(id);
            if (productType != null)
            {
                productTypeRepository.Delete(productType);
                await productTypeRepository.SaveChangesAsync();
            }
        }
    }
}
