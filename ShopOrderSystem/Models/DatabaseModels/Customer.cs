using ShopOrderSystem.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ShopOrderSystem.Models.DatabaseModels
{
    public class Customer : ISoftDelete
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; }
        public bool IsDeleted { get; set; }
    }

}
