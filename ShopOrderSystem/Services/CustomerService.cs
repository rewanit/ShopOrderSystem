using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopOrderSystem.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await customerRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            await customerRepository.AddAsync(customer);
            await customerRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            customerRepository.Update(customer);
            await customerRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer != null)
            {
                customerRepository.Delete(customer);
                await customerRepository.SaveChangesAsync();
            }
        }
    }
}
