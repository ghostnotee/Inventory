using FluentValidation;

namespace Inventory.Application.Features.Users.Commands.CreateUser;

public class LoginUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(i => i.EmailAddress).NotNull()
            .EmailAddress()
            .WithMessage("{PropertyName} not a valid email address");
        RuleFor(i => i.Password).NotNull()
            .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLenght} characters");
    }
}