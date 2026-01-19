using InventorySales.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.Entities.Common
{
    public abstract class AuditableEntity<T> : BaseEntity<T>
    {
        public string CreatedById { get; protected set; }
    
    }
}
