using InventorySales.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Infrastructure.Exceptions
{
    public class ConcurrencyException : BaseException
    {
        public ConcurrencyException()
           : base($"The entity was modified by another user. Please retry.")
        {
        }
    }
}
