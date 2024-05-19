using ShopOrderSystem.Models.Interfaces;

namespace ShopOrderSystem.Models.DatabaseModels
{
    public class Order : ISoftDelete
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public bool IsDeleted { get; set; }

    }

}
