using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Infrastructure.ModelBuilders;

public class UserModelBuilder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(x => x.ExternalId)
              .IsRequired()
              .HasMaxLength(100);

        builder.HasIndex(x => x.ExternalId)
              .IsUnique();

        builder.Property(x => x.CreatedDate)
              .IsRequired();

    }
}
