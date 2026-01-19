using InventorySales.Application.Base;

namespace InventorySales.Application.Exceptions
{
    public class UserIdNotFoundException : BaseException
    {
        public UserIdNotFoundException()
            : base($"User ID was not found.")
        {
        }
    }
}
