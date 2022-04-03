using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.entity;

namespace shopapp.data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(i => new {i.ProductId, i.CategoryId});

            // Bu özellik "ModelBuilderExtensions"da yapıldı.
            // builder.HasData(
            //     // PHONE
            //     new ProductCategory() {ProductId=1,CategoryId=1},
            //     new ProductCategory() {ProductId=2,CategoryId=1},
            //     new ProductCategory() {ProductId=3,CategoryId=1},
            //     new ProductCategory() {ProductId=4,CategoryId=1},
            //     new ProductCategory() {ProductId=5,CategoryId=1},
            //     new ProductCategory() {ProductId=6,CategoryId=1},
            //     new ProductCategory() {ProductId=7,CategoryId=1},
            //     new ProductCategory() {ProductId=8,CategoryId=1},
            //     new ProductCategory() {ProductId=9,CategoryId=1},
            //     new ProductCategory() {ProductId=10,CategoryId=1},
            //     new ProductCategory() {ProductId=11,CategoryId=1},
            //     new ProductCategory() {ProductId=12,CategoryId=1},
            //     new ProductCategory() {ProductId=13,CategoryId=1},
            //     // LAPTOP                
            //     new ProductCategory() {ProductId=14,CategoryId=2},
            //     new ProductCategory() {ProductId=15,CategoryId=2},
            //     new ProductCategory() {ProductId=16,CategoryId=2},
            //     new ProductCategory() {ProductId=17,CategoryId=2},
            //     new ProductCategory() {ProductId=18,CategoryId=2},
            //     new ProductCategory() {ProductId=19,CategoryId=2},
            //     new ProductCategory() {ProductId=20,CategoryId=2},
            //     new ProductCategory() {ProductId=21,CategoryId=2},
            //     new ProductCategory() {ProductId=22,CategoryId=2},
            //     new ProductCategory() {ProductId=23,CategoryId=2},
            //     new ProductCategory() {ProductId=24,CategoryId=2},
            //     // Desktop Computer                
            //     new ProductCategory() {ProductId=25,CategoryId=3},
            //     new ProductCategory() {ProductId=26,CategoryId=3},
            //     new ProductCategory() {ProductId=27,CategoryId=3},
            //     new ProductCategory() {ProductId=28,CategoryId=3},
            //     // Tablet
                
            //     new ProductCategory() {ProductId=29,CategoryId=4},
            //     new ProductCategory() {ProductId=30,CategoryId=4},
            //     new ProductCategory() {ProductId=31,CategoryId=4}
            // );
        }
    }
}