using InventorySales.Application.Base;

namespace InventorySales.Application.Exceptions
{
    public class RefreshTokenInvalidException : BaseException
    {
        public RefreshTokenInvalidException() : base("Refresh token is invalid or expired.")
        {
        }
    }
}
