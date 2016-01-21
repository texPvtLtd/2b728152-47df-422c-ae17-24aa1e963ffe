using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Entities.Models.Mapping;

namespace Entities.Models
{
    public partial class UmartContext : DbContext
    {
        static UmartContext()
        {
            Database.SetInitializer<UmartContext>(null);
        }

        public UmartContext()
            : base("Name=UmartContext")
        {
        }

        public DbSet<AddToCart> AddToCarts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddToCartMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CustomerDetailMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
