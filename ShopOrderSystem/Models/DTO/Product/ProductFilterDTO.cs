using System.ComponentModel.DataAnnotations;

namespace ShopOrderSystem.Models.DTO.Product
{
    /// <summary>
    /// DTO для фильтрации и сортировки товаров
    /// </summary>
    public class ProductFilterDTO
    {
        /// <summary>
        /// Идентификатор типа товара для фильтрации
        /// </summary>
        public int? ProductTypeId { get; set; }

        /// <summary>
        /// Флаг наличия на складе для фильтрации
        /// </summary>
        public bool? InStock { get; set; }

        /// <summary>
        /// Порядок сортировки по цене (asc или desc)
        /// </summary>
        [RegularExpression("asc|desc", ErrorMessage = "SortOrder must be 'asc' or 'desc'")]
        public string SortOrder { get; set; } = "asc";
    }
}
