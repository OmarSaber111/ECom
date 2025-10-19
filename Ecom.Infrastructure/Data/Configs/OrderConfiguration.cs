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
    public class OrderConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(o => o.shippingAddress, sh => { sh.WithOwner(); });
            builder.HasMany(o => o.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(o=>o.status).HasConversion(o=>o.ToString(),o=>(Status)Enum.Parse(typeof(Status),o));
            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
