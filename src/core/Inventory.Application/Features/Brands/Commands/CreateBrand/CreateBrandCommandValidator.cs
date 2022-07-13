using FluentValidation;

namespace Inventory.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(i => i.Name).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
    }
}