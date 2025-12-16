namespace InventorySales.Application.Features.Products.Commands.DecreaseStock
{
    public class DecreaseStockValidator : AbstractValidator<DecreaseStockCommand>
    {
        public DecreaseStockValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId boş olamaz");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Azaltma miktarı sıfırdan büyük olmalı");
        }
    }
}
