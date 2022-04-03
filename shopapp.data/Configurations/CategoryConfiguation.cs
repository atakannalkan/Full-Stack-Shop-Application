using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.entity;

namespace shopapp.data.Configurations
{
    public class CategoryConfiguation : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(i => i.CategoryId); // Primary Key
            
            // Bu özellik "ModelBuilderExtensions"da yapıldı.
            // builder.HasData(
            //     new Category() {CategoryId=1,Name="Telefon",Url="telefon"},            
            //     new Category() {CategoryId=2,Name="Laptop",Url="laptop"},
            //     new Category() {CategoryId=3,Name="Masaüstü Bilgisayar",Url="bilgisayar"},
            //     new Category() {CategoryId=4,Name="Tablet",Url="tablet"}
            // );
        }
    }
}