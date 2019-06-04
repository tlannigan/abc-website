using ca.abcmufflerandhitch.Models;
using ca.abcmufflerandhitch.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ca.abcmufflerandhitch.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Electrical> Electricals { get; set; }
        public DbSet<Exhaust> Exhausts { get; set; }
        public DbSet<Hitch> Hitches { get; set; }
        public DbSet<Suspension> Suspensions{ get; set; }
    }
}
