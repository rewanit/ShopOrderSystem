namespace ShopOrderSystem.Models.DatabaseModels
{
    public class OrderItemDetail
    {
        public int Id { get; set; }
        public int OrderItemId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public virtual OrderItem OrderItem { get; set; }
    }
}
