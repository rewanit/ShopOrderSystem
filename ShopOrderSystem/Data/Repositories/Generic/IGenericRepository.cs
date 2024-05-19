using Microsoft.EntityFrameworkCore;

namespace ShopOrderSystem.Data.Repositories.Generic
{
    public interface IGenericRepository<T, TId> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TId id);
        Task AddAsync(params T[] entity);
        void Update(params T[] entity);
        void Delete(params T[] entity);
        Task SaveChangesAsync();
    }
}
