using FluentValidation;

namespace Inventory.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("{PropertyName} cannot be null or empty")
            .NotNull().WithMessage("{PropertyName} cannot be null or empty");
    }
}