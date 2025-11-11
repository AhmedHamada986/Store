using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Data.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethode>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethode> builder)
        {
            builder.Property(D => D.ShortName).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(D => D.Description).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(D=>D.DeliveryTime).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(D => D.Price).HasColumnType("decimal(18,2)");

        }
    }
}
