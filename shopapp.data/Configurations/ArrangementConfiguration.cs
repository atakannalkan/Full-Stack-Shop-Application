using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.entity;

namespace shopapp.data.Configurations
{
    public class ArrangementConfiguration : IEntityTypeConfiguration<Arrangement>
    {
        public void Configure(EntityTypeBuilder<Arrangement> builder)
        {
            builder.HasKey(i => i.Id); // Primary Key

            // Bu özellik "ModelBuilderExtensions"da yapıldı.
            // builder.HasData(
            //     new Arrangement() {Id=1,Content=4}
            // );
        }
    }
}