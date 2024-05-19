using ShopOrderSystem.Data.Repositories;
using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Services.Interfaces;

namespace ShopOrderSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            productRepository.Update(product);
            await productRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product != null)
            {
                productRepository.Delete(product);
                await productRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(int? productTypeId, bool? inStock, string sortOrder)
        {
            return await productRepository.GetFilteredProductsAsync(productTypeId, inStock, sortOrder);
        }
    }
}
