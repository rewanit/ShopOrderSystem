using ShopOrderSystem.Models.DatabaseModels;
using ShopOrderSystem.Models.DTO.Customer;
using ShopOrderSystem.Models.DTO.Order;
using ShopOrderSystem.Models.DTO.OrderItem;
using ShopOrderSystem.Models.DTO.Product;
using ShopOrderSystem.Models.DTO.ProductType;

namespace ShopOrderSystem.Data.Mappings
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            // Маппинг для Customer
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            // Маппинг для Order
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(x=>x.Quantity, x=>x.MapFrom(l=>l.OrderItemDetail.Quantity))
                .ForMember(x=>x.Price, x=>x.MapFrom(l=>l.OrderItemDetail.Price));
            // Маппинг для Product
            CreateMap<Product,ProductDTO>().ReverseMap();
            // Маппинг для ProductType
            CreateMap<ProductType, ProductTypeDTO>().ReverseMap();




        }
    }
}
