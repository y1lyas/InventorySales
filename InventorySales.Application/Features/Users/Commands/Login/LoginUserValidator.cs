namespace InventorySales.Application.Features.Users.Commands.Login
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş olamaz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş olamaz");
        }
    }
}
