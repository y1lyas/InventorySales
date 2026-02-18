using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş olamaz").MinimumLength(1)
                .WithMessage("Kategori adı en az 1 karakter olmalı");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Kategori açıklaması boş olamaz");
        }
    }
}
