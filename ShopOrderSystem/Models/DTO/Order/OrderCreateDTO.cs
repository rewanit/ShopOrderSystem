using System.ComponentModel.DataAnnotations;

namespace ShopOrderSystem.Models.DTO.Order
{
    /// <summary>
    /// DTO для создания заказа
    /// </summary>
    public class OrderCreateDTO
    {
        /// <summary>
        /// Идентификатор клиента, для которого создается заказ
        /// </summary>
        [Required(ErrorMessage = "CustomerId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be greater than zero")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Словарь, содержащий идентификаторы товаров и их количество в заказе
        /// </summary>
        [Required(ErrorMessage = "OrderItems are required")]
        [MinLength(1, ErrorMessage = "OrderItems must contain at least one item")]
        public Dictionary<int, int> OrderItems { get; set; }
    }
}
