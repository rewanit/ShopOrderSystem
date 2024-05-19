using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Models.DTO.Order;
using ShopOrderSystem.Services.Interfaces;
using ShopOrderSystem.Utility.Exceptions;

namespace ShopOrderSystem.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех заказов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await orderService.GetAllAsync();
            var orderDtos = mapper.Map<IEnumerable<OrderDTO>>(orders);
            return Ok(orderDtos);
        }

        /// <summary>
        /// Получение заказа по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await orderService.GetFullOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDto = mapper.Map<OrderDTO>(order);
            return Ok(orderDto);
        }

        /// <summary>
        /// Создание нового заказа
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderCreateDTO createOrderDto)
        {
            try
            {
                var order = await orderService.CreateOrder(createOrderDto.CustomerId, createOrderDto.OrderItems);
                //order = await orderService.GetFullOrder(order.OrderId);
                var orderResultDto = mapper.Map<OrderDTO>(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, orderResultDto);

            }
            catch (NotEnoughException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }    

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await orderService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Получение списка заказов по конкретному клиенту за выбранный временной период
        /// </summary>
        /// <param name="customerId">ID клиента</param>
        /// <param name="startDate">Начальная дата</param>
        /// <param name="endDate">Конечная дата</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByCustomerAndDateRange(int customerId, DateTime startDate, DateTime endDate)
        {
            var orders = await orderService.GetOrdersByCustomerAndDateRangeAsync(customerId, startDate, endDate);
            var orderDtos = mapper.Map<IEnumerable<OrderDTO>>(orders);
            return Ok(orderDtos);
        }



    }
}
