using Microsoft.EntityFrameworkCore;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data.Seed
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().HasData(GetProductTypes());
            modelBuilder.Entity<Product>().HasData(GetProducts());
            modelBuilder.Entity<Customer>().HasData(GetCustomers());
            modelBuilder.Entity<Order>().HasData(GetOrders());
            modelBuilder.Entity<OrderItem>().HasData(GetOrderItems());
            modelBuilder.Entity<OrderItemDetail>().HasData(GetOrderItemDetails());
        }

        private static List<ProductType> GetProductTypes()
        {
            return new List<ProductType>
            {
                new ProductType { ProductTypeId = 1, TypeName = "Электроника" },
                new ProductType { ProductTypeId = 2, TypeName = "Одежда" },
                new ProductType { ProductTypeId = 3, TypeName = "Бытовая техника" },
                new ProductType { ProductTypeId = 4, TypeName = "Косметика" },
                new ProductType { ProductTypeId = 5, TypeName = "Игрушки" }
            };
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { ProductId = 1, ProductTypeId = 1, ProductName = "Смартфон", Price = 500, AvailableQuantity = 10 },
                new Product { ProductId = 2, ProductTypeId = 2, ProductName = "Футболка", Price = 20, AvailableQuantity = 50 },
                new Product { ProductId = 3, ProductTypeId = 3, ProductName = "Холодильник", Price = 1000, AvailableQuantity = 5 },
                new Product { ProductId = 4, ProductTypeId = 4, ProductName = "Крем для лица", Price = 30, AvailableQuantity = 20 },
                new Product { ProductId = 5, ProductTypeId = 5, ProductName = "Конструктор LEGO", Price = 50, AvailableQuantity = 30 }
            };
        }

        private static List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { CustomerId = 1, FullName = "Иванов Иван Иванович", Phone = "+1234567890" },
                new Customer { CustomerId = 2, FullName = "Петров Петр Петрович", Phone = "+0987654321" },
                new Customer { CustomerId = 3, FullName = "Сидоров Сидор Сидорович", Phone = "+1112223333" },
                new Customer { CustomerId = 4, FullName = "Кузнецова Анастасия Михайловна", Phone = "+5556667777" },
                new Customer { CustomerId = 5, FullName = "Смирнов Алексей Сергеевич", Phone = "+8889990000" }
            };
        }

        private static List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order { OrderId = 1, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-10) },
                new Order { OrderId = 2, CustomerId = 2, OrderDate = DateTime.Now.AddDays(-5) },
                new Order { OrderId = 3, CustomerId = 3, OrderDate = DateTime.Now.AddDays(-3) },
                new Order { OrderId = 4, CustomerId = 4, OrderDate = DateTime.Now.AddDays(-2) },
                new Order { OrderId = 5, CustomerId = 5, OrderDate = DateTime.Now.AddDays(-1) }
            };
        }

        private static List<OrderItem> GetOrderItems()
        {
            return new List<OrderItem>
            {
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1 },
                new OrderItem { Id = 2, OrderId = 2, ProductId = 2 },
                new OrderItem { Id = 3, OrderId = 3, ProductId = 3 },
                new OrderItem { Id = 4, OrderId = 4, ProductId = 4 },
                new OrderItem { Id = 5, OrderId = 5, ProductId = 5 }
            };
        }

        private static List<OrderItemDetail> GetOrderItemDetails()
        {
            // Детали заказанных товаров, например, цена и количество
            return new List<OrderItemDetail>
            {
                new OrderItemDetail { Id = 1, OrderItemId = 1, Price = 500, Quantity = 2 },
                new OrderItemDetail { Id = 2, OrderItemId = 2, Price = 20, Quantity = 3 },
                new OrderItemDetail { Id = 3, OrderItemId = 3, Price = 1000, Quantity = 1 },
                new OrderItemDetail { Id = 4, OrderItemId = 4, Price = 30, Quantity = 5 },
                new OrderItemDetail { Id = 5, OrderItemId = 5, Price = 50, Quantity = 2 }
            };
        }
    }
}
