using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Models.DTO.Customer;
using ShopOrderSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopOrderSystem.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await customerService.GetAllAsync();
            var customerDtos = mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return Ok(customerDtos);
        }

        /// <summary>
        /// Получение клиента по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = await customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerDto = mapper.Map<CustomerDTO>(customer);
            return Ok(customerDto);
        }

        /// <summary>
        /// Создание нового клиента
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CustomerDTO customerDto)
        {
            var customer = mapper.Map<Customer>(customerDto);
            await customerService.AddAsync(customer);
            var customerResultDto = mapper.Map<CustomerDTO>(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customerResultDto);
        }

        /// <summary>
        /// Обновление клиента
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateCustomer(int id, CustomerDTO customerDto)
        {
            if (id != customerDto.CustomerId)
            {
                return BadRequest();
            }

            var existingCustomer = await customerService.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            var customer = mapper.Map<Customer>(customerDto);
            await customerService.UpdateAsync(customer);
            return NoContent();
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await customerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
