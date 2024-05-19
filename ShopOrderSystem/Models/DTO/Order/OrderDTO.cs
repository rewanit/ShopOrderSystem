using System.ComponentModel.DataAnnotations;
using ShopOrderSystem.Models.DTO.OrderItem;

namespace ShopOrderSystem.Models.DTO.Order
{
    /// <summary>
    /// DTO для представления заказа
    /// </summary>
    public class OrderDTO
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Идентификатор клиента, к которому привязан заказ
        /// </summary>
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        [Required(ErrorMessage = "OrderDate is required")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Список элементов заказа
        /// </summary>
        [Required(ErrorMessage = "OrderItems are required")]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
