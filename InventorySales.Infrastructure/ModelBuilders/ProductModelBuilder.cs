using InventorySales.Domain.Entities;

namespace InventorySales.Infrastructure.ModelBuilders;

public class ProductModelBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
