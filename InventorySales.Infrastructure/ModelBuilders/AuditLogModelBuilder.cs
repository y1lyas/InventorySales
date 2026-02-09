using InventorySales.Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.ModelBuilders
{
    public class AuditLogModelBuilder : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.EntityName)
           .IsRequired();

            builder.Property(x => x.EntityId)
                .IsRequired();

            builder.Property(x => x.Action)
                .IsRequired();

            builder.Property(x => x.PerformedBy)
                .IsRequired();

            builder.Property(x => x.CorrelationId)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.EntityName);
            builder.HasIndex(x => x.EntityId);
            builder.HasIndex(x => x.CreatedAt);
            builder.HasIndex(x => x.CorrelationId);
        }
    }
}
