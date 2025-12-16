namespace InventorySales.Application.Features.Products.Commands.UpdatePrice
{
    public class UpdatePriceValidator : AbstractValidator<UpdateProductPriceCommand>
    {
        public UpdatePriceValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId boş olamaz");
            RuleFor(x => x.NewPrice).GreaterThan(0).WithMessage("Yeni fiyat sıfırdan büyük olmalı");
        }
    }
}
