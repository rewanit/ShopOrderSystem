using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Services.Interfaces
{
    public interface ICustomerService
    {
        Task AddAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task UpdateAsync(Customer customer);
    }
}