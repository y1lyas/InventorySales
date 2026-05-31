using InventorySales.Application.Exceptions.Base;

namespace InventorySales.Application.Common.Exceptions
{
    public class CategoryNotFoundException : BaseException
    {
        public CategoryNotFoundException(Guid? categoryId) : base($"Category with ID '{categoryId}' was not found.")
        {
        }
    }
}
