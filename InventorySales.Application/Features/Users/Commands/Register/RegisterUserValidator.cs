namespace InventorySales.Application.Features.Users.Commands.Register
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş olamaz")
                   .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş olamaz")
                .MinimumLength(8).WithMessage("Parola en az 8 karakter olmalı");
        }
    }
}
