using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.UnassignCategory
{
    public class UnassignCategoryValidator : AbstractValidator<UnassignCategoryCommand>
    {
        public UnassignCategoryValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Kategori ID'si boş olamaz");
        }
    }
}
