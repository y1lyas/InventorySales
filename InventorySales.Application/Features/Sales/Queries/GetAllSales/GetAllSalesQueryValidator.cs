using InventorySales.Application.Features.Sales.Queries.GetSales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Sales.Queries.GetAllSales
{
    public class GetAllSalesQueryValidator
    : AbstractValidator<GetAllSalesQuery>
    {
        public GetAllSalesQueryValidator()
        {
            RuleFor(x => x.MinAmount)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinAmount.HasValue);

            RuleFor(x => x.MaxAmount)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxAmount.HasValue);

            RuleFor(x => x)
                .Must(x =>
                    !x.MinAmount.HasValue ||
                    !x.MaxAmount.HasValue ||
                    x.MinAmount <= x.MaxAmount)
                .WithMessage("MinAmount cannot be greater than MaxAmount.");

            RuleFor(x => x)
                .Must(x =>
                    !x.StartDate.HasValue ||
                    !x.EndDate.HasValue ||
                    x.StartDate <= x.EndDate)
                .WithMessage("StartDate cannot be later than EndDate.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 200);
        }
    }
}
