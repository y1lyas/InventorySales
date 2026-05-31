using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Commands.UpdateName
{
    public class UpdateNameValidator : AbstractValidator<UpdateProductNameCommand>
    {
        public UpdateNameValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId boş olamaz");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Yeni isim boş olamaz");
        }
    }
}
