namespace InventorySales.Application.Features.Sales.Commands.MakeSale
{
    public class MakeSaleValidator : AbstractValidator<SaleItemRequest>
    {
        public MakeSaleValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId boş olamaz");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Satış miktarı sıfırdan büyük olmalı");
        }
    }
}
