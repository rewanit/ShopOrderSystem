using ShopOrderSystem.Data.Repositories.Interfaces;
using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Services.Interfaces;
using ShopOrderSystem.Utility.Exceptions;

namespace ShopOrderSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderItemDetailRepository orderItemDetailRepository;

        public OrderService(
            IOrderRepository orderRepository, 
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository,
            IOrderItemDetailRepository orderItemDetailRepository)
        {
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.productRepository = productRepository;
            this.orderItemDetailRepository = orderItemDetailRepository;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await orderRepository.GetAllAsync();
        }

        public async Task<Order> GetFullOrder (int id)
        {
            return await orderRepository.GetFullAsync(id);
        }

        public async Task<Order> CreateOrder(int customerId, Dictionary<int,int> orders)
        {   
            var unavailableProducts = await productRepository.UnavailableProducts(orders);
            if (unavailableProducts.Any()) throw new NotEnoughException(unavailableProducts.Select(x=>x.ProductName));

            var requestedProducts = await productRepository.GetAllByIdsAsync(orders.Select(x => x.Key).ToArray());
            var requestedProductsDict = requestedProducts.ToDictionary(x => x.ProductId, x => x);
            var orderItems = new List<OrderItem>();
            var orderItemDetails = new List<OrderItemDetail>();
            foreach (var orderItem in orders)
            {
                var newOrderItem = new OrderItem()
                {
                    ProductId = orderItem.Key,
                };

                var newOrderItemDetail = new OrderItemDetail
                {
                    OrderItem = newOrderItem,
                    Price = requestedProductsDict[orderItem.Key].Price,
                    Quantity = orderItem.Value
                };

                orderItems.Add(newOrderItem);
                orderItemDetails.Add(newOrderItemDetail);
            }

            var newOrder = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = customerId,
                OrderItems = orderItems
            };
            await productRepository.UpdateProductsQuantity(orders);
            await orderItemDetailRepository.AddAsync(orderItemDetails.ToArray());
            await orderItemRepository.AddAsync(orderItems.ToArray());
            await orderRepository.AddAsync(newOrder);
            await orderItemRepository.SaveChangesAsync();
            return newOrder;
        }


        public async Task DeleteAsync(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                orderRepository.Delete(order);
                await orderRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAndDateRangeAsync(int customerId, DateTime startDate, DateTime endDate)
        {
            return await orderRepository.GetOrdersByCustomerAndDateRangeAsync(customerId, startDate, endDate);
        }

    }
}
