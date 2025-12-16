using InventorySales.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.Entities.Auth
{
    public class Role : BaseEntity<Guid>
    {
        public string Name { get; private set; }
        private Role() { }
        public Role(string name) 
        { 
            Name = name;
        }
    }
}
