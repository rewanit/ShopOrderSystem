using ShopOrderSystem.Models.Interfaces;

namespace ShopOrderSystem.Models.DatabaseModels
{
    public class Product : ISoftDelete
    {
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public required string ProductName { get; set; }
        public float Price { get; set; }
        public int AvailableQuantity { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public bool IsDeleted { get; set; }

    }

}
