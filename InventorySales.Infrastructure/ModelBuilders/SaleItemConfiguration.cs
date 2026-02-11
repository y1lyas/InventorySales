using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.ModelBuilders
{
    public class SaleItemModelBuilder : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ComplexProperty(x => x.Quantity, prop =>
            {
                prop.Property(q => q.Value).HasColumnName("Quantity");
            });

            builder.ComplexProperty(x => x.UnitPriceAtSale, prop =>
            {
                prop.Property(m => m.Amount).HasColumnName("UnitPrice");
                prop.Property(m => m.Currency).HasColumnName("Currency").HasMaxLength(3);
            });

            builder.HasOne<Product>()
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
