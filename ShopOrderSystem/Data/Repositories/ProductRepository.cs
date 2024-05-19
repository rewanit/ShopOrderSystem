using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ShopOrderSystem.Data.Repositories.Generic;
using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;
using System.Collections;

namespace ShopOrderSystem.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ShopOrderSystemDbContext context, IMemoryCache memoryCache) : base(context, memoryCache)
        {
        }

        public async Task<IEnumerable<Product>> UnavailableProducts(Dictionary<int, int> products)
        {
            var productIds = products.Keys.ToList();

            var unavailableProducts = await context.Products
                .AsNoTracking()
                .Where(x => productIds.Contains(x.ProductId))
                .ToListAsync();

            unavailableProducts = unavailableProducts
                .Where(x => x.AvailableQuantity < products[x.ProductId])
                .ToList();

            return unavailableProducts;
        }

        public async Task UpdateProductsQuantity(Dictionary<int, int> productIdQuantityMap)
        {
            var productIdsList = productIdQuantityMap.Select(x => x.Key).ToList();
            var productsToUpdate = await context.Products
                .Where(p => productIdsList.Contains(p.ProductId))
                .ToListAsync();

            foreach (var product in productsToUpdate)
            {
                product.AvailableQuantity -= productIdQuantityMap[product.ProductId];
            }
            context.Products.UpdateRange(productsToUpdate);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(params int[] ids)
        {
            return await context.Products.AsNoTracking().Where(x => ids.Contains(x.ProductId)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(int? productTypeId, bool? inStock, string sortOrder)
        {
            var query = context.Products.AsQueryable();

            if (productTypeId.HasValue)
            {
                query = query.Where(p => p.ProductTypeId == productTypeId.Value);
            }

            if (inStock.HasValue)
            {
                query = query.Where(p => inStock.Value ? p.AvailableQuantity > 0 : p.AvailableQuantity == 0);
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                query = sortOrder.ToLower() == "asc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
