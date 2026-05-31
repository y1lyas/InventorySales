namespace InventorySales.Application.Features.Products.Commands.RemoveProduct
{
    public class RemoveProductValidator : AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductValidator()
        {
            RuleFor(x => x.productId).NotEmpty().WithMessage("ProductId boş olamaz");
        }
    }
}
