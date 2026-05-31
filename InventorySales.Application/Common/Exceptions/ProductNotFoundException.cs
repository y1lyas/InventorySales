using InventorySales.Application.Exceptions.Base;

namespace InventorySales.Application.Common.Exceptions
{
    public class ProductNotFoundException : BaseException
    {
        public ProductNotFoundException(Guid productId)
            : base($"Product with ID '{productId}' was not found.")
        {
        }
    }
}
