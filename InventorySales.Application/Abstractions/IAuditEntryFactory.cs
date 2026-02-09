using InventorySales.Domain.Entities.System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Abstractions
{
    public interface IAuditEntryFactory
    {
        AuditLog Create(EntityEntry entry);
    }

}
