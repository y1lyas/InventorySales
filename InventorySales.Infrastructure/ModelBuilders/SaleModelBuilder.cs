using InventorySales.Domain.Entities;

namespace InventorySales.Infrastructure.ModelBuilders;

public class SaleModelBuilder : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasOne<Product>()
                 .WithMany()
                 .HasForeignKey(s => s.ProductId);

        builder.OwnsOne(s => s.TotalPrice, propBuilder =>
        {
            propBuilder.Property(m => m.Amount).HasColumnName("TotalPrice");
            propBuilder.Property(m => m.Currency).HasColumnName("Currency");
        });

        builder.OwnsOne(s => s.Quantity, qty =>
        {
            qty.Property(q => q.Value).HasColumnName("QuantityAmount");
        });
    }
}
