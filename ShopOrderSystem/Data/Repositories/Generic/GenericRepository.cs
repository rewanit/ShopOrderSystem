using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace ShopOrderSystem.Data.Repositories.Generic
{
    public abstract class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
    {
        protected readonly ShopOrderSystemDbContext context;
        protected readonly IMemoryCache memoryCache;
        private readonly DbSet<T> dbSet;

        public GenericRepository(ShopOrderSystemDbContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            this.memoryCache = memoryCache;
            dbSet = this.context.Set<T>();
        }

        protected string GetRepositoryCacheName()
        {
            var t= string.Join(" ",this.GetType().Name, typeof(T).Name);
            Debug.WriteLine(t);
            return t;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (memoryCache.TryGetValue(GetRepositoryCacheName(), out IEnumerable<T> cachedData))
            {
                return cachedData;
            }
            else
            {
                var newData = await dbSet.ToListAsync();
                memoryCache.Set(GetRepositoryCacheName(), newData, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(10)});
                return newData;
            }
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddAsync(params T[] entity)
        {
            memoryCache.Remove(GetRepositoryCacheName());
            await dbSet.AddRangeAsync(entity);
        }

        public void Update(params T[] entity)
        {
            dbSet.UpdateRange(entity);
        }

        public void Delete(params T[] entity)
        {
            dbSet.RemoveRange(entity);
        }

        public async Task SaveChangesAsync()
        {
            memoryCache.Remove(GetRepositoryCacheName());
            await context.SaveChangesAsync();
        }
    }
}
