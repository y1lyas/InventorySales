using InventorySales.Domain.Entities.Auth;

namespace InventorySales.Infrastructure.ModelBuilders
{
    public class RoleModelBuilder : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
