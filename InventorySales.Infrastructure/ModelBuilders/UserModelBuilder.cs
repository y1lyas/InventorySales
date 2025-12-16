using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Infrastructure.ModelBuilders;

public class UserModelBuilder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.RefreshTokens).WithOne();
        builder.HasMany(u => u.Roles).WithMany();
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);
    }
}
