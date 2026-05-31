using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetAllStockMovements
{
    public class GetAllStockMovementsValidator : AbstractValidator<GetAllStockMovementsQuery>
    {
        public GetAllStockMovementsValidator() 
        {
            RuleFor(x => x.MinQuantity)
        .GreaterThanOrEqualTo(0)
        .When(x => x.MinQuantity.HasValue);

            RuleFor(x => x.MaxQuantity)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxQuantity.HasValue);

            RuleFor(x => x)
                .Must(x =>
                    !x.MinQuantity.HasValue ||
                    !x.MaxQuantity.HasValue ||
                    x.MinQuantity <= x.MaxQuantity)
                .WithMessage("MinQuantity cannot be greater than MaxQuantity.");
        }
    }
}
