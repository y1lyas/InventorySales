using InventorySales.Application.Features.Products.Queries.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryValidator
    : AbstractValidator<GetAllProductsQuery>
    {
        public GetAllProductsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 200);

            RuleFor(x => x.MinStock)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinStock.HasValue);

            RuleFor(x => x.MaxStock)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxStock.HasValue);

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
                .Must(x =>
                    !x.MinStock.HasValue ||
                    !x.MaxStock.HasValue ||
                    x.MinStock <= x.MaxStock)
                .WithMessage(
                    "MinStock cannot be greater than MaxStock.");

            RuleFor(x => x)
                .Must(x =>
                    !x.MinPrice.HasValue ||
                    !x.MaxPrice.HasValue ||
                    x.MinPrice <= x.MaxPrice)
                .WithMessage(
                    "MinPrice cannot be greater than MaxPrice.");

            RuleFor(x => x)
                .Must(x =>
                    !x.StartDate.HasValue ||
                    !x.EndDate.HasValue ||
                    x.StartDate <= x.EndDate)
                .WithMessage(
                    "StartDate cannot be later than EndDate.");
        }
    }
}
