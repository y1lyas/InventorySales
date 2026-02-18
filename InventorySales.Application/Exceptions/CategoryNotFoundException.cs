using InventorySales.Application.Exceptions.Base;
using InventorySales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Exceptions
{
    public class CategoryNotFoundException : BaseException
    {
        public CategoryNotFoundException(Guid? categoryId) : base($"Category with ID '{categoryId}' was not found.")
        {
        }
    }
}
