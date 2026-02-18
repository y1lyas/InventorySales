using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Abstractions
{
    public interface ICorrelationContext
    {
         string Id { get; }
    }
}
