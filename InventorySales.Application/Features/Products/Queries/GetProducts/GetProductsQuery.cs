using InventorySales.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery() : IRequest<List<ProductDto>>;


}
