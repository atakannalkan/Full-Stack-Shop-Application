using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.entity;

namespace shopapp.data.Configurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.HasKey(i => i.Id); // Primary Key

            // Bu özellik "ModelBuilderExtensions"da yapıldı.
            // builder.HasData(
            //     new Slider() {Id=1,ImageUrl="slider1.png",Active=true,Order=1},
            //     new Slider() {Id=2,ImageUrl="slider2.png", Active=false, Order=2},
            //     new Slider() {Id=3,ImageUrl="slider3.png", Active=false, Order=3},
            //     new Slider() {Id=4,ImageUrl="slider4.jpg", Active=false, Order=4}
            // );
        }
    }
}