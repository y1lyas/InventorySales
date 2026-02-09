using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities.System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.Auditing
{
    public class AuditEntryFactory : IAuditEntryFactory
    {
        private readonly IUserService _userService;
        private readonly ICorrelationContext _correlationContext;

        public AuditEntryFactory(
            IUserService userService,
            ICorrelationContext correlationContext)
        {
            _userService = userService;
            _correlationContext = correlationContext;
        }
        public AuditLog Create(EntityEntry entry)
        {
            return new AuditLog
            {
                Id = Guid.NewGuid(),
                EntityName = entry.Entity.GetType().Name,
                EntityId = GetEntityId(entry),
                Action = entry.State.ToString(),
                ChangedProperties = GetChangedProperties(entry),
                PerformedBy = _userService.UserId ?? "SYSTEM",
                CorrelationId = _correlationContext.Id,
                CreatedAt = DateTime.UtcNow
            };
        }
        private static string GetEntityId(EntityEntry entry)
        {
            var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
            return key?.CurrentValue?.ToString() ?? "UNKNOWN";
        }
        private static string? GetChangedProperties(EntityEntry entry)
        {
            if (entry.State != EntityState.Modified)
                return null;

            var changedProps = entry.Properties
                .Where(p => p.IsModified)
                .Select(p => p.Metadata.Name)
                .ToList();

            return changedProps.Count == 0
                ? null
                : JsonSerializer.Serialize(changedProps);
        }
    }
}
