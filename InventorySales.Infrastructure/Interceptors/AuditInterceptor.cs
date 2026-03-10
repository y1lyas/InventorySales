using InventorySales.Application.Abstractions.Services;
using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Entities.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly IUserService _userService;

        public AuditInterceptor(IUserService userService)
        {
            _userService = userService;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateAuditEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditEntities(DbContext? context)
        {
            if (context == null) return;
            var userId = _userService.UserId;
            var now = DateTime.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity baseEntity && entry.State == EntityState.Added)
                {
                    if (!string.IsNullOrEmpty(userId))
                        baseEntity.CreatedById = userId;
                }

                if (entry.Entity is IAuditable auditableEntity && entry.State == EntityState.Modified)
                {
                    auditableEntity.ModifiedAt = now;
                    if (!string.IsNullOrEmpty(userId))
                        auditableEntity.ModifiedById = userId;
                }
            }
        }
    }
}
