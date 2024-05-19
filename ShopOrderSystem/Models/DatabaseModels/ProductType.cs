using ShopOrderSystem.Models.Interfaces;

namespace ShopOrderSystem.Models.DatabaseModels
{
    public class ProductType : ISoftDelete
    {
        public int ProductTypeId { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public bool IsDeleted { get; set; }

    }

}
