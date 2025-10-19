using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Configs
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price)
                .HasColumnType("decimal(18,2)");
            builder.HasData(new DeliveryMethod { Id = 1, Name= "DHL",Price=15,DeliveryTime="only a week", Description ="the fast delivery in the world" },
                                 new DeliveryMethod { Id = 2, Name = "Fodex", Price = 20, DeliveryTime = "only take two week", Description = "make your product save" });
        }
    }
}
