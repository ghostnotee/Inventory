using FluentValidation;
using Inventory.Application.Interfaces.Repositories;

namespace Inventory.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator(IBrandRepository brandRepository)
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("{PropertyName} cannot be null or empty")
            .NotNull().WithMessage("{PropertyName} cannot be null or empty");
    }
}