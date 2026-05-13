using InventorySales.Application.Abstractions;
using InventorySales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IUnitOfWork _uow;
        public CreateCategoryValidator(IUnitOfWork uow)
        {
            _uow = uow;

            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Kategori adı boş olamaz")
             .MinimumLength(1).WithMessage("Kategori adı en az 1 karakter olmalı")
             .MustAsync(BeUniqueName).WithMessage("Bu kategori adı zaten kullanımda");

            RuleFor(x => x.Description)
            .MaximumLength(300)
            .Must(description => string.IsNullOrWhiteSpace(description) || description.Trim().Length >= 2)
            .WithMessage("Description must be at least 3 characters long.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken ct)
        {
            var exists = await _uow.Repository<Category>().Query()
                .AnyAsync(x => x.Name.ToLower() == name.ToLower(), ct);

            return !exists;
        }
    }
}
