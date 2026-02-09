using InventorySales.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Exceptions
{
    public class UserIdFromHttpContextNotFound : BaseException
    {
        public UserIdFromHttpContextNotFound() : base("User Id not found")
        {
        }
    }
}
