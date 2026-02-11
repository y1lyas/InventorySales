using InventorySales.Domain.Entities;

namespace InventorySales.Infrastructure.ModelBuilders;

public class ProductModelBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.RowVersion)
           .IsRowVersion(); 

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
