using InventorySales.Application.Exceptions.Base;

namespace InventorySales.Application.Common.Exceptions
{
    public class UserIdFromHttpContextNotFound : BaseException
    {
        public UserIdFromHttpContextNotFound() : base("User Id not found")
        {
        }
    }
}
