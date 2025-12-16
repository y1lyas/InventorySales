namespace InventorySales.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı boş olamaz")
                .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalı")
                .MaximumLength(100).WithMessage("Ürün adı 100 karakterden uzun olamaz");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Birim fiyat sıfırdan büyük olmalı");
        }
    }
}
