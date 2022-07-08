using FluentValidation;

namespace Inventory.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandValidator: AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(i => i.Name).NotNull().WithMessage("{PropertyName} cannot be null");
        RuleFor(i => i.Id).NotNull().WithMessage("{PropertyName} cannot be null");
    }
}