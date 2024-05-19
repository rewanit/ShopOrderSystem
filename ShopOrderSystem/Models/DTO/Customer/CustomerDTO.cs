using System.ComponentModel.DataAnnotations;

namespace ShopOrderSystem.Models.DTO.Customer
{
    /// <summary>
    /// DTO для представления клиента
    /// </summary>
    public class CustomerDTO
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// ФИО клиента
        /// </summary>
        [Required(ErrorMessage = "FullName is required")]
        [StringLength(100, ErrorMessage = "FullName can't be longer than 100 characters")]
        public string FullName { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(15, ErrorMessage = "Phone can't be longer than 15 characters")]
        public string Phone { get; set; }
    }
}
