using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Interfaces
{
    public interface IUserService
    {
        public Guid UserId { get; }
    }
}
