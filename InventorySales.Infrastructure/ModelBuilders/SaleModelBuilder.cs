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
    }
}
