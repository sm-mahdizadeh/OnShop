using FluentValidation;
using OnShop.Domain.User.Commands;

namespace OnShop.ApplicationServices.User.Command
{
    public class CreateUserInfoCommandValidator : AbstractValidator<AddApplicationUserInfoCommand>
    {
        public CreateUserInfoCommandValidator()
        {
            RuleFor(x => x.NationalCode).MinimumLength(10).MaximumLength(10).WithErrorCode("");
        }
    }
}