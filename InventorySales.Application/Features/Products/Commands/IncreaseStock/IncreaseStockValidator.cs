namespace InventorySales.Application.Features.Products.Commands.IncreaseStock
{
    public class IncreaseStockValidator : AbstractValidator<IncreaseStockCommand>
    {
        public IncreaseStockValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId boş olamaz");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Artış miktarı sıfırdan büyük olmalı");
        }
    }
}
