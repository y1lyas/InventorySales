using InventorySales.Domain.Entities;

namespace InventorySales.Infrastructure.ModelBuilders;

public class StockMovementModelBuilder : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.HasKey(sm => sm.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.HasOne(x => x.Product)
               .WithMany(p => p.StockMovements)
               .HasForeignKey(x => x.ProductId);




    }
}
