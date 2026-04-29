using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Commands.RemoveProduct
{
    public class RemoveProductValidator : AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductValidator()
        {
            RuleFor(x => x.productId).NotEmpty().WithMessage("ProductId boş olamaz");
        }
    }
}
