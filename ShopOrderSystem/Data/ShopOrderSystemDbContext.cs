using Microsoft.EntityFrameworkCore;
using ShopOrderSystem.Data.Interceptors;
using ShopOrderSystem.Data.Seed;
using ShopOrderSystem.Models.DatabaseModels;

namespace ShopOrderSystem.Data
{
    public class ShopOrderSystemDbContext : DbContext
    {
        public ShopOrderSystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<OrderItemDetail> OrderItemDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Делаем фильтр для удаленных записей
            modelBuilder.Entity<Customer>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Order>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ProductType>().HasQueryFilter(x => x.IsDeleted == false);

            modelBuilder.Seed();

        }


    }
}
