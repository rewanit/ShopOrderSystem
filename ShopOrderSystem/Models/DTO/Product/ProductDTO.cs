using System.ComponentModel.DataAnnotations;

namespace ShopOrderSystem.Models.DTO.Product
{
    /// <summary>
    /// DTO для представления товара
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Идентификатор типа товара
        /// </summary>
        [Required(ErrorMessage = "ProductTypeId is required")]
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        [Required(ErrorMessage = "ProductName is required")]
        [StringLength(100, ErrorMessage = "ProductName can't be longer than 100 characters")]
        public string ProductName { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number")]
        public float Price { get; set; }

        /// <summary>
        /// Доступное количество товара
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "AvailableQuantity must be a positive number")]
        public int AvailableQuantity { get; set; }
    }
}
