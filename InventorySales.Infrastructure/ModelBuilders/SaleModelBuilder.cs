using InventorySales.Domain.Entities;

namespace InventorySales.Infrastructure.ModelBuilders;

public class SaleModelBuilder : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(s => s.Id);

        builder.OwnsOne(s => s.TotalPrice, propBuilder =>
        {
            propBuilder.Property(m => m.Amount).HasColumnName("TotalPrice");
            propBuilder.Property(m => m.Currency).HasColumnName("Currency");
        });

        builder.HasMany(s => s.Items)
               .WithOne()
               .HasForeignKey("SaleId") // shadow Fk
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        var navigation = builder.Metadata.FindNavigation(nameof(Sale.Items));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
