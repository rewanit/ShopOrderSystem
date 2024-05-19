using System.ComponentModel.DataAnnotations;

namespace ShopOrderSystem.Models.DTO.ProductType
{
    /// <summary>
    /// Data Transfer Object для типа продукта
    /// </summary>
    public class ProductTypeDTO
    {
        /// <summary>
        /// Идентификатор типа продукта
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Название типа продукта
        /// </summary>
        [Required(ErrorMessage = "Название типа продукта обязательно.")]
        [StringLength(100, ErrorMessage = "Название типа продукта не должно превышать 100 символов.")]
        public string TypeName { get; set; }
    }
}
