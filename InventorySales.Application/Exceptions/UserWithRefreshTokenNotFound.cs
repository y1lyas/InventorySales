using InventorySales.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Exceptions
{
    public class UserWithRefreshTokenNotFound : BaseException
    {
        public UserWithRefreshTokenNotFound() : base("User with the specified refresh token was not found.")
        {
        }
    }
}
