using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Configs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.NewPrice)
                .HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product { Id = 1, Name = "Test", Description = "Test", NewPrice = 999.99m, CategoryId = 1 }
            );

        }
    }
}
