using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.AssignCategory
{
    public class AssignCategoryValidatior : AbstractValidator<AssignCategoryCommand>
    {
        public AssignCategoryValidatior()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required");
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required");
        }


    }
}
