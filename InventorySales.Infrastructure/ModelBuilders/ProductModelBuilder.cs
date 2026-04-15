using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace InventorySales.Infrastructure.ModelBuilders;

public class ProductModelBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);

        builder.Property(p => p.RowVersion)
           .IsRowVersion();

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.OwnsOne(p => p.Price, p => {
            p.Property(m => m.Amount).HasColumnName("Price_Amount");
            p.Property(m => m.Currency).HasColumnName("Price_Currency");
        });

        builder.OwnsOne(p => p.Stock, p => {
            p.Property(q => q.Value).HasColumnName("CurrentStock");
        });

        builder.OwnsOne(p => p.Sku, skuBuilder =>
        {
            skuBuilder.Property(s => s.Value)
                .HasColumnName("SKU")
                .IsRequired()
                .HasMaxLength(50);

            skuBuilder.HasIndex(s => s.Value).IsUnique();
        });
    }
}
