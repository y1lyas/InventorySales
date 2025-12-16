using InventorySales.Application.Base;

namespace InventorySales.Application.Exceptions
{
    public class EmailOrPasswordInvalidException : BaseException
    {
        public EmailOrPasswordInvalidException()
            : base("The email or password provided is invalid.")
        {
        }
    }
}
