using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.data.Configurations;
using shopapp.entity;

namespace shopapp.data.Concrete
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Arrangement> Arrangement { get; set; }
        public DbSet<Cart> Carts { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("Data Source=myShopAppDb");
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Burada her bir metodu çağırdım ancak yorum satırısna alarak toplu oluşturduğum "ModelBuilderExtensions" metodunu aşağıda kullandım. 
            modelBuilder.ApplyConfiguration(new ArrangementConfiguration()); // Fluent Api kodlarımı çağırdım.
            modelBuilder.ApplyConfiguration(new SliderConfiguration()); // Fluent Api kodlarımı çağırdım.
            modelBuilder.ApplyConfiguration(new ProductConfiguration()); // Fluent Api kodlarımı çağırdım.
            modelBuilder.ApplyConfiguration(new CategoryConfiguation()); // Fluent Api kodlarımı çağırdım.
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration()); // Fluent Api kodlarımı çağırdım.

            // Toplu Fluent Api kodları
            modelBuilder.Seed();

            // modelBuilder.Entity<ProductCategory>()
            //             .HasKey(i => new {i.ProductId, i.CategoryId});
        }
    }
}