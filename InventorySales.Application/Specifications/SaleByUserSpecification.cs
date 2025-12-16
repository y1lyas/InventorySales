using InventorySales.Application.Specifications.Base;
using InventorySales.Domain.Entities;


namespace InventorySales.Application.Specifications
{
    public class SaleByUserSpecification: BaseSpecification<Sale>
    {
        public SaleByUserSpecification(Guid userId)
        {
            Criteria = s => s.CreatedById == userId;
        }
    }
}
