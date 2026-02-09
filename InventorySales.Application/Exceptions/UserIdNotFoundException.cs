using InventorySales.Application.Exceptions.Base;

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
