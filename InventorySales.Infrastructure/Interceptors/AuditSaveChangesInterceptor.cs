using InventorySales.Application.Abstractions;
using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities.System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.Interceptors
{
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IUserService _userService;
        private readonly ICorrelationContext _correlationContext;
        private readonly IAuditEntryFactory _factory;

        public AuditSaveChangesInterceptor(
            IUserService userService,
            ICorrelationContext correlationContext,
            IAuditEntryFactory factory)
        {
            _userService = userService;
            _correlationContext = correlationContext;
            _factory = factory;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var auditLogs = context.ChangeTracker.Entries()
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted)
            .Where(e => e.Entity is not AuditLog)
            .Select(e => _factory.Create(e))
            .ToList();

            if (auditLogs.Any())
                context.Set<AuditLog>().AddRange(auditLogs);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
