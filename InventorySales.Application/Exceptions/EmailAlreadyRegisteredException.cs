using InventorySales.Application.Base;

namespace InventorySales.Application.Exceptions
{
    public class EmailAlreadyRegisteredException : BaseException
    {
        public EmailAlreadyRegisteredException(string email)
            : base($"The email '{email}' is already registered.")
        {
        }
    }
}
