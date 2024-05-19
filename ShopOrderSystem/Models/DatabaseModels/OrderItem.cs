using ShopOrderSystem.Models.Interfaces;

namespace ShopOrderSystem.Models.DatabaseModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual OrderItemDetail OrderItemDetail { get; set; }
    }

}
